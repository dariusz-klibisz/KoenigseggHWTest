using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using canlibCLSNET;
using Kvaser.Kvadblib;

namespace KoenigseggHWTest
{
    public partial class KoenigseggHWTest : Form
    {
        private const UInt16 DEFAULT_FRAME_ID = 2016;
        private TestFrame CANFrame = new TestFrame(aID: DEFAULT_FRAME_ID);
        private static List<Node> nodes = new List<Node>();

        private enum PinCfgFunction
        {
            PIN_CFG_FUNCTION,
            PIN_CFG_OUTPUT,
            PIN_CFG_INPUT,
            PIN_CFG_DRIVE_STR,
            PIN_CFG_OPEN_DRAIN,
            PIN_CFG_HYSTERESIS,
            PIN_CFG_SLEW_RATE,
            PIN_CFG_PULL_CTRL,
            PIN_CFG_PULL_SELECT,
            PIN_CFG_MAX
        }

        private UInt16[] PinCfgShift =
        {
            10,     /* PIN_CFG_FUNCTION */
            9,      /* PIN_CFG_OUTPUT */
            8,      /* PIN_CFG_INPUT */
            6,      /* PIN_CFG_DRIVE_STR */
            5,      /* PIN_CFG_OPEN_DRAIN */
            4,      /* PIN_CFG_HYSTERESIS */
            2,      /* PIN_CFG_SLEW_RATE */
            1,      /* PIN_CFG_PULL_CTRL */
            0       /* PIN_CFG_PULL_SELECT */
        };

        private UInt16[] PinCfgMask =
        {
            0x1C00, /* PIN_CFG_FUNCTION */
            0x0200, /* PIN_CFG_OUTPUT */
            0x0100, /* PIN_CFG_INPUT */
            0x00C0, /* PIN_CFG_DRIVE_STR */
            0x0020, /* PIN_CFG_OPEN_DRAIN */
            0x0010, /* PIN_CFG_HYSTERESIS */
            0x000C, /* PIN_CFG_SLEW_RATE */
            0x0002, /* PIN_CFG_PULL_CTRL */
            0x0001  /* PIN_CFG_PULL_SELECT */
        };

        public KoenigseggHWTest()
        {
            InitializeComponent();

            Canlib.canStatus status;
            int channel = 0;
            int chanhandle;
            byte[] data = new byte[8];
            int msgId = 200;
            int msgFlags = 0;

            //Initialize, open channel and go on bus
            Canlib.canInitializeLibrary();

            chanhandle = Canlib.canOpenChannel(channel, Canlib.canOPEN_ACCEPT_VIRTUAL);
            DisplayError((Canlib.canStatus)chanhandle, "canSetBusParams");

            status = Canlib.canSetBusParams(chanhandle, Canlib.canBITRATE_250K, 0, 0, 0, 0, 0);
            DisplayError(status, "canSetBusParams");

            status = Canlib.canBusOn(chanhandle);
            DisplayError(status, "canBusOn");

            Canlib.canWriteWait(chanhandle, msgId, data, 8, msgFlags, 50);

            //Load database
            Kvadblib.Status dbstatus;
            Kvadblib.Hnd dbhandle = new Kvadblib.Hnd();

            string filename = "C:\\Repos\\KoenigseggHWTest\\Regera_HSCAN1.dbc";
            dbstatus = Kvadblib.Open(out dbhandle);
            DisplayDBError(dbstatus, "Opening database handle ");
            dbstatus = Kvadblib.ReadFile(dbhandle, filename);
            DisplayDBError(dbstatus, "Reading database from file ");

            ReadDbNodes(dbhandle);

            ReadDbFrames(dbhandle);

            UpdateRestbusNodesListBox();

            Debug.Print("\n========================\n");
            foreach (Node node in nodes)
            {
                Debug.Print(node.ToString());
            }
            Debug.Print("\n========================\n");

            //Go off bus and close channel
            status = Canlib.canBusOff(chanhandle);
            DisplayError(status, "canBusOff");

            status = Canlib.canClose(chanhandle);
            DisplayError(status, "canClose");
        }

        private void setPinCfgBits(UInt16 value, PinCfgFunction function)
        {
            /* Get all bits except pin function bits. */
            UInt16 data = (UInt16)(CANFrame.GetPinConfig() & (~PinCfgMask[(UInt16)function]));
            /* Add chosen pin function bits. */
            data |= (UInt16)((value << PinCfgShift[(UInt16)function]) & PinCfgMask[(UInt16)function]);
            CANFrame.SetPinConfig(data);
        }

        private void frameIdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (true == frameIdHexCheckBox.Checked)
            {
                frameIDNumericUpDown.Hexadecimal = true;
                frameIdTextBox.Visible = true;
            }
            else
            {
                frameIDNumericUpDown.Hexadecimal = false;
                frameIdTextBox.Visible = false;
            }
        }

        private void frameIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UInt16 idInt = (UInt16)frameIDNumericUpDown.Value;
                CANFrame.SetID(idInt);
                Debug.Print(CANFrame.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse ID'{frameIDNumericUpDown.Value}'");
            }
        }

        private void pinNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UInt16 pinNrInt = UInt16.Parse(pinNumberComboBox.Text);
                CANFrame.SetPinNumber(pinNrInt);
                Debug.Print(CANFrame.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{pinNumberComboBox.Text}'");
            }
        }

        private void framePeriodNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UInt16 framePeriodInt = (UInt16)framePeriodNumericUpDown.Value;
                CANFrame.SetPeriod(framePeriodInt);
                Debug.Print(CANFrame.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{framePeriodNumericUpDown.Value}'");
            }
        }

        private void udsRoutineNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Byte udsRoutineIByte = (Byte)udsRoutineNumericUpDown.Value;
                CANFrame.SetUDSRoutine(udsRoutineIByte);
                Debug.Print(CANFrame.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{udsRoutineNumericUpDown.Value}'");
            }
        }

        private void pinCfgRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton radioButton = (RadioButton)sender;
                setPinCfgBits((UInt16)radioButton.TabIndex, (PinCfgFunction)radioButton.Parent.TabIndex);
                Debug.Print(CANFrame.ToString());
            }
        }

        private void startTransmisionButton_Click(object sender, EventArgs e)
        {
            
        }

        //Displays error messages when a call to Canlib fails
        private static void DisplayError(Canlib.canStatus status, string method)
        {
            if (status < 0)
            {
                Console.WriteLine(method + " failed: " + status.ToString());
            }
        }

        //Displays messages for Kvadblib calls
        private static void DisplayDBError(Kvadblib.Status status, string method)
        {
            if (status < 0)
            {
                Console.WriteLine(method + " failed: " + status.ToString());
            }
        }

        private void ReadDbNodes(Kvadblib.Hnd aDbHnd)
        {
            Kvadblib.NodeHnd nh = new Kvadblib.NodeHnd();

            Debug.Print("Nodes: \n");
            if (Kvadblib.Status.OK == Kvadblib.GetFirstNode(aDbHnd, out nh))
            {
                ReadNode(nh);
            }

            while (Kvadblib.Status.OK == Kvadblib.GetNextNode(aDbHnd, out nh))
            {
                ReadNode(nh);
            }
        }

        private static void ReadNode(Kvadblib.NodeHnd aNodeHnd)
        {
            string name = "";

            Kvadblib.GetNodeName(aNodeHnd, out name);
            nodes.Add(new Node(name, aNodeHnd));
            Debug.Print(name);
        }

        private static void ReadDbFrames(Kvadblib.Hnd aDbHnd)
        {
            Kvadblib.MessageHnd mh = new Kvadblib.MessageHnd();

            Debug.Print("\nMessages: \n");
            if (Kvadblib.Status.OK == Kvadblib.GetFirstMsg(aDbHnd, out mh))
            {
                ReadFrame(mh);
            }

            while (Kvadblib.Status.OK == Kvadblib.GetNextMsg(aDbHnd, out mh))
            {
                ReadFrame(mh);
            }
        }

        private static void ReadFrame(Kvadblib.MessageHnd aMsgHnd)
        {
            Kvadblib.NodeHnd nh = new Kvadblib.NodeHnd();
            string name = "";
            string senderNode = "";
            int frameID = 0;
            int frameByteLength = 0;
            Kvadblib.MESSAGE frameFlags;

            Kvadblib.GetMsgName(aMsgHnd, out name);
            Kvadblib.GetMsgId(aMsgHnd, out frameID, out frameFlags);
            Kvadblib.GetMsgSendNode(aMsgHnd, out nh);
            Kvadblib.GetNodeName(nh, out senderNode);
            Kvadblib.GetMsgDlc(aMsgHnd, out frameByteLength);
            Debug.Print(name);
            foreach (Node node in nodes)
            {
                if (node.GetName() == senderNode)
                {
                    node.AddFrame(new Frame((UInt16)frameID, name, aByteLength: (Byte)frameByteLength, aHandle: aMsgHnd));
                    ReadDbSignals(aMsgHnd, node);
                }
            }
        }

        private static void ReadDbSignals(Kvadblib.MessageHnd aMsgHnd, Node aNode)
        {
            Kvadblib.SignalHnd sh = new Kvadblib.SignalHnd();

            if (Kvadblib.Status.OK == Kvadblib.GetFirstSignal(aMsgHnd, out sh))
            {
                ReadSignal(sh, aMsgHnd, aNode);
            }

            while (Kvadblib.Status.OK == Kvadblib.GetNextSignal(aMsgHnd, out sh))
            {
                ReadSignal(sh, aMsgHnd, aNode);
            }
        }

        private static void ReadSignal(Kvadblib.SignalHnd aSgnHnd, Kvadblib.MessageHnd aMsgHnd, Node aNode)
        {
            string name = "";
            List<string> receiverNode = new List<string>();
            int size = 0;
            int startBit = 0;
            double factor = 0.0;
            double offset = 0.0;
            double min = 0.0;
            double max = 0.0;
            string unit = "";
            Kvadblib.AttributeHnd ah = new Kvadblib.AttributeHnd();
            Kvadblib.NodeHnd nh;
            Kvadblib.SignalType pt;
            Kvadblib.SignalType rt;

            Kvadblib.GetSignalName(aSgnHnd, out name);
            Kvadblib.GetSignalValueSize(aSgnHnd, out startBit, out size);
            Kvadblib.GetSignalValueScaling(aSgnHnd, out factor, out offset);
            Kvadblib.GetSignalValueLimits(aSgnHnd, out min, out max);
            Kvadblib.GetSignalUnit(aSgnHnd, out unit);
            Kvadblib.GetSignalPresentationType(aSgnHnd, out pt);
            Kvadblib.GetSignalRepresentationType(aSgnHnd, out rt);

            /* Fill receiverNode list. Each signal can have multiple receive nodes. */
            foreach (Node node in nodes)
            {
                nh = node.GetNodeHandle();
                Kvadblib.Status status = Kvadblib.SignalContainsReceiveNode(aSgnHnd, nh);
                if (Kvadblib.Status.OK == status)
                {
                    string nodeName = node.GetName();
                    if (!receiverNode.Contains(nodeName))
                    {
                        receiverNode.Add(nodeName);
                    }
                }
            }

            /* Add signal to correct frame in node. */
            /* Get Frame name from handle. */
            Kvadblib.GetMsgName(aMsgHnd, out string frameName);
            /* Get frame hanle from node based on frame name. */
            aNode.GetFrame(frameName, out Frame fh);
            /* Add signal to frame. */
            fh.AddSignal(new Signal(name, (UInt16)startBit, (UInt16)size, factor, offset, min, max, unit, aSgnHnd));



            //TODO: Update functions with Status
            //TODO: Add Signal byte order
            //TODO: Add handling setting Signal values
            //TODO: Add setting Frame data when signal value is set (call Frame function from Signal?)
            //TODO: Add processing txt file as input to automated sending of frames
            //TODO: Add displaying log file with sent frames and time
            //TODO: Fix VS Messages

            //Kvadblib.GetFirstSignalAttribute(sh, ref ah);
            //Kvadblib.GetAttributeName(ah, out name);
            Debug.Print(name);
        }

        private void RestbusNodesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRestbusFramesListBox(sender, e);
        }

        private void UpdateRestbusNodesListBox()
        {
            //Update Nodes names in list
            RestbusNodesListBox.BeginUpdate();
            RestbusNodesListBox.Items.Clear();
            foreach (Node node in nodes)
            {
                RestbusNodesListBox.Items.Add(node);
            }
            RestbusNodesListBox.EndUpdate();
            //Select 1st element in Nodes list
            RestbusNodesListBox.SelectedIndex = 0;
            if (nodes.Count > 0)
            {
                RestbusFramesListBox.SelectedIndex = 0;
            }
            else
            {
                RestbusFramesListBox.Items.Clear();
                RestbusSignalsListBox.Items.Clear();
            }
        }

        private void RestbusFramesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRestbusSignalsListBox(sender, e);
            //Update frame properties displayed
            UpdateRestbusFrameProperties(sender, e);
        }

        private void UpdateRestbusFramesListBox(object sender, EventArgs e)
        {
            //Get selected Node
            Node selectedNode = (Node)RestbusNodesListBox.SelectedItem;
            UInt32 nrOfFrames = selectedNode.GetNrOfFrames();
            //Update Frames names in list
            RestbusFramesListBox.BeginUpdate();
            RestbusFramesListBox.Items.Clear();
            for(UInt32 idx = 0; idx < nrOfFrames; idx++)
            {
                Frame frame = new Frame();
                Status.ErrorCode status = selectedNode.GetFrame(idx, out frame);
                if((Status.ErrorCode.STATUS_OK == status) && (null != frame))
                {
                    RestbusFramesListBox.Items.Add(frame);
                }
            }
            RestbusFramesListBox.EndUpdate();
            //Select 1st element in Frames list
            if(nrOfFrames > 0)
            {
                RestbusFramesListBox.SelectedIndex = 0;
            }
            else
            {
                RestbusSignalsListBox.Items.Clear();
            }
        }

        private void UpdateRestbusFrameProperties(object sender, EventArgs e)
        {
            //Get selected Frame
            Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
            //Update properties
            frameIDNumericUpDown.Value = selectedFrame.GetID();
            framePeriodNumericUpDown.Value = selectedFrame.GetPeriod();
            frameDLCComboBox.Text = selectedFrame.GetByteLength().ToString();

        }


        private void RestbusSignalsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update signal properties displayed
            UpdateRestbusSignalProperties(sender, e);
        }

        private void UpdateRestbusSignalsListBox(object sender, EventArgs e)
        {
            //Get selected Frame
            Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
            UInt32 nrOfSignals = selectedFrame.GetNrOfSignals();
            //Update Signals names in list
            RestbusSignalsListBox.BeginUpdate();
            RestbusSignalsListBox.Items.Clear();
            for (UInt32 idx = 0; idx < nrOfSignals; idx++)
            {
                Signal signal = new Signal();
                Status.ErrorCode status = selectedFrame.GetSignal(idx, out signal);
                if ((Status.ErrorCode.STATUS_OK == status) && (null != signal))
                {
                    RestbusSignalsListBox.Items.Add(signal);
                }
            }
            RestbusSignalsListBox.EndUpdate();
            //Select 1st element in Signals list
            if (nrOfSignals > 0)
            {
                RestbusSignalsListBox.SelectedIndex = 0;
            }
        }

        private void UpdateRestbusSignalProperties(object sender, EventArgs e)
        {
            //Get selected Frame
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            //Update properties
            signalStartBitNumericUpDown.Value = selectedSignal.GetStartBit();
            signalBitLengthNumericUpDown.Value = selectedSignal.GetBitLength();
            signalScaleTextBox.Text = selectedSignal.GetScale().ToString();
            signalOffsetTextBox.Text = selectedSignal.GetOffset().ToString();
            signalMinTextBox.Text = selectedSignal.GetMin().ToString();
            signalMaxTextBox.Text = selectedSignal.GetMax().ToString();
            signalUnitTextBox.Text = selectedSignal.GetUnit().ToString();
            signalValueTextBox.Text = selectedSignal.GetValue().ToString();
            signalRawValueTextBox.Text = selectedSignal.GetRawValue().ToString();
        }

        private void dataHexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (true == dataHexCheckBox.Checked)
            {
                dataB7NumericUpDown.Hexadecimal = true;
                dataB6NumericUpDown.Hexadecimal = true;
                dataB5NumericUpDown.Hexadecimal = true;
                dataB4NumericUpDown.Hexadecimal = true;
                dataB3NumericUpDown.Hexadecimal = true;
                dataB2NumericUpDown.Hexadecimal = true;
                dataB1NumericUpDown.Hexadecimal = true;
                dataB0NumericUpDown.Hexadecimal = true;
                frameDataTextBox.Visible = true;
            }
            else
            {
                dataB7NumericUpDown.Hexadecimal = false;
                dataB6NumericUpDown.Hexadecimal = false;
                dataB5NumericUpDown.Hexadecimal = false;
                dataB4NumericUpDown.Hexadecimal = false;
                dataB3NumericUpDown.Hexadecimal = false;
                dataB2NumericUpDown.Hexadecimal = false;
                dataB1NumericUpDown.Hexadecimal = false;
                dataB0NumericUpDown.Hexadecimal = false;
                frameDataTextBox.Visible = false;
            }
        }

        private void frameDLCComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumericUpDown[] dataBytes =
            {
                dataB0NumericUpDown,
                dataB1NumericUpDown,
                dataB2NumericUpDown,
                dataB3NumericUpDown,
                dataB4NumericUpDown,
                dataB5NumericUpDown,
                dataB6NumericUpDown,
                dataB7NumericUpDown
            };

            Label[] dataByteLabels =
            {
                dataB0Label,
                dataB1Label,
                dataB2Label,
                dataB3Label,
                dataB4Label,
                dataB5Label,
                dataB6Label,
                dataB7Label
            };

            try
            {
                Byte frameDLC = Byte.Parse(frameDLCComboBox.Text);
                for (Byte dlc = Frame.FRAME_LENGTH_MAX; dlc > 1; dlc--)
                {
                    if (frameDLC < dlc)
                    {
                        dataBytes[dlc - 1].Visible = false;
                        dataBytes[dlc - 1].Enabled = false;
                        dataByteLabels[dlc - 1].Visible = false;
                        dataByteLabels[dlc - 1].Enabled = false;
                    }
                    else
                    {
                        dataBytes[dlc - 1].Visible = true;
                        dataBytes[dlc - 1].Enabled = true;
                        dataByteLabels[dlc - 1].Visible = true;
                        dataByteLabels[dlc - 1].Enabled = true;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse DLC'{frameDLCComboBox.Text}'");
            }
        }
    }
}