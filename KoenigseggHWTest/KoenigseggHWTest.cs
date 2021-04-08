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
using System.IO;
using System.Security;

//TODO: Update functions with Status
//TODO: Add handling setting Signal values
//TODO: Add setting Frame data when signal value is set (call Frame function from Signal?)
//TODO: Add processing txt file as input to automated sending of frames
//TODO: Add displaying log file with sent frames and time
//TODO: Fix VS Messages
//TODO: Add creating new nodes, frames, signals
//TODO: Add saving configuration from menu item
//TODO: Better handling of signal encoding
//TODO: Combine functions for handling changing signal and frame values from window
//TODO: Handle receiver node list for signal
//TODO: Handle signal presentation type and representation type
//TODO: Add handling of log window, open file, step, run, stop, add frame to log
//TODO: Add verifying log when opened file
//TODO: Send the frame on step button press
//TODO: Fix the sending of frames on step to two channels
//TODO: Add exception handling when parsing line on step
//TODO: Improve performance of analyzing lines and sending frames from textbox
//TODO: Add protections from running if file was not analyzed
//TODO: Add protections when textbox is empty

namespace KoenigseggHWTest
{
    public partial class KoenigseggHWTest : Form
    {
        private const UInt16 DEFAULT_FRAME_ID = 2016;
        private const Byte CAN_CHANNEL_NR = 1;
        private TestFrame CANFrame = new TestFrame(aID: DEFAULT_FRAME_ID);
        private static List<Node> nodes = new List<Node>();
        private static List<LogFrame> logFrames = new List<LogFrame>();
        private int[] canChanHdl = new int[CAN_CHANNEL_NR];

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

        private readonly UInt16[] PinCfgShift =
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

        private readonly UInt16[] PinCfgMask =
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
            byte[] data = new byte[8];
            int msgId = 200;
            int msgFlags = 0;

            //Initialize, open channel and go on bus
            Canlib.canInitializeLibrary();

            for (int ch = 0; ch < CAN_CHANNEL_NR; ch++)
            {
                canChanHdl[ch] = Canlib.canOpenChannel(ch, Canlib.canOPEN_ACCEPT_VIRTUAL);
                DisplayError((Canlib.canStatus)canChanHdl[ch], "canSetBusParams");

                status = Canlib.canSetBusParams(canChanHdl[ch], Canlib.canBITRATE_500K, 0, 0, 0, 0, 0);
                DisplayError(status, "canSetBusParams");

                status = Canlib.canBusOn(canChanHdl[ch]);
                DisplayError(status, "canBusOn");
            }

            Canlib.canWriteWait(canChanHdl[0], msgId, data, 8, msgFlags, 50);

            Debug.Print("\n========================\n");
        }

        private void SetPinCfgBits(UInt16 value, PinCfgFunction function)
        {
            /* Get all bits except pin function bits. */
            UInt16 data = (UInt16)(CANFrame.GetPinConfig() & (~PinCfgMask[(UInt16)function]));
            /* Add chosen pin function bits. */
            data |= (UInt16)((value << PinCfgShift[(UInt16)function]) & PinCfgMask[(UInt16)function]);
            CANFrame.SetPinConfig(data);
        }

        private void FrameIdCheckBox_CheckedChanged(object sender, EventArgs e)
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

        private void FrameIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UInt16 idInt = (UInt16)frameIDNumericUpDown.Value;
                // Get selected Frame
                Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
                // Set selected frame ID
                selectedFrame.SetID(idInt);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse frame ID'{frameIDNumericUpDown.Value}'");
            }
        }

        private void PinNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void FramePeriodNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UInt16 framePeriodInt = (UInt16)framePeriodNumericUpDown.Value;
                // Get selected Frame
                Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
                // Set selected frame period
                selectedFrame.SetPeriod(framePeriodInt);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse frame period'{framePeriodNumericUpDown.Value}'");
            }
        }

        private void UdsRoutineNumericUpDown_ValueChanged(object sender, EventArgs e)
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

        private void PinCfgRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton radioButton = (RadioButton)sender;
                SetPinCfgBits((UInt16)radioButton.TabIndex, (PinCfgFunction)radioButton.Parent.TabIndex);
                Debug.Print(CANFrame.ToString());
            }
        }

        private void StartTransmisionButton_Click(object sender, EventArgs e)
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
            Kvadblib.GetNodeName(aNodeHnd, out string name);
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

            Kvadblib.GetMsgName(aMsgHnd, out string name);
            Kvadblib.GetMsgId(aMsgHnd, out int frameID, out Kvadblib.MESSAGE frameFlags);
            Kvadblib.GetMsgSendNode(aMsgHnd, out nh);
            Kvadblib.GetNodeName(nh, out string senderNode);
            Kvadblib.GetMsgDlc(aMsgHnd, out int frameByteLength);
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
            if (Kvadblib.Status.OK == Kvadblib.GetFirstSignal(aMsgHnd, out Kvadblib.SignalHnd sh))
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
            List<string> receiverNode = new List<string>();

            Kvadblib.GetSignalName(aSgnHnd, out string name);
            Kvadblib.GetSignalValueSize(aSgnHnd, out int startBit, out int size);
            Kvadblib.GetSignalValueScaling(aSgnHnd, out double factor, out double offset);
            Kvadblib.GetSignalValueLimits(aSgnHnd, out double min, out double max);
            Kvadblib.GetSignalUnit(aSgnHnd, out string unit);
            Kvadblib.GetSignalPresentationType(aSgnHnd, out Kvadblib.SignalType pt);
            Kvadblib.GetSignalRepresentationType(aSgnHnd, out Kvadblib.SignalType rt);
            Kvadblib.GetSignalEncoding(aSgnHnd, out Kvadblib.SignalEncoding encoding);

            /* Fill receiverNode list. Each signal can have multiple receive nodes. */
            foreach (Node node in nodes)
            {
                Kvadblib.NodeHnd nh = node.GetNodeHandle();
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
            fh.AddSignal(new Signal(name, (Byte)startBit, (Byte)size, factor, offset, min, max, unit, encoding, aSgnHnd));

            //Kvadblib.GetFirstSignalAttribute(sh, ref ah);
            //Kvadblib.GetAttributeName(ah, out name);
            Debug.Print(name);
        }

        private void RestbusNodesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRestbusFramesListBox(sender, e);
            //Update frame properties displayed
            UpdateRestbusNodeProperties(sender, e);
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

        private void UpdateRestbusNodeProperties(object sender, EventArgs e)
        {
            //Get selected Node
            Node selectedNode = (Node)RestbusNodesListBox.SelectedItem;
            //Update properties
            nodeNameTextBox.Text = selectedNode.GetName();
            nodeEnableTxCheckBox.Checked = selectedNode.GetEnableTransmission();
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
                Status.ErrorCode status = selectedNode.GetFrame(idx, out Frame frame);
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

            //Get selected Frame
            Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
            //Update properties
            frameNameTextBox.Text = selectedFrame.GetName();
            frameIDNumericUpDown.Value = selectedFrame.GetID();
            framePeriodNumericUpDown.Value = selectedFrame.GetPeriod();
            frameDLCComboBox.Text = selectedFrame.GetByteLength().ToString();
            frameEnableTxCheckBox.Checked = selectedFrame.GetEnableTransmission();
            for (Byte idx = 0; idx < Frame.FRAME_LENGTH_MAX; idx++)
            {
                dataBytes[idx].Value = selectedFrame.GetData(idx);
            }
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
                Status.ErrorCode status = selectedFrame.GetSignal(idx, out Signal signal);
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
            //Get selected Signal
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            //Update properties
            signalNameTextBox.Text = selectedSignal.GetName();
            signalStartBitNumericUpDown.Value = selectedSignal.GetStartBit();
            signalBitLengthNumericUpDown.Value = selectedSignal.GetBitLength();
            signalScaleTextBox.Text = selectedSignal.GetScale().ToString();
            signalOffsetTextBox.Text = selectedSignal.GetOffset().ToString();
            signalMinTextBox.Text = selectedSignal.GetMin().ToString();
            signalMaxTextBox.Text = selectedSignal.GetMax().ToString();
            signalUnitTextBox.Text = selectedSignal.GetUnit().ToString();
            signalValueTextBox.Text = selectedSignal.GetValue().ToString();
            signalRawValueTextBox.Text = selectedSignal.GetRawValue().ToString();
            signalEncodingComboBox.Text = selectedSignal.GetEncoding().ToString();
        }

        private void DataHexCheckBox_CheckedChanged(object sender, EventArgs e)
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

        private void FrameDLCComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
                // Get selected Frame
                Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
                // Set selected frame DLC
                selectedFrame.SetByteLength(frameDLC);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse frame DLC '{frameDLCComboBox.Text}'");
            }
        }

        private void OpenCanLogButton_Click(object sender, EventArgs e)
        {
            if (openCanLogFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the data to text box
                    var sr = new StreamReader(openCanLogFileDialog.FileName);
                    canLogTextBox.Text = sr.ReadToEnd();
                    // Select the first line
                    int idx = canLogTextBox.GetFirstCharIndexFromLine(0);
                    string line = canLogTextBox.Lines[0];
                    int length = line.Length;
                    canLogTextBox.Select(idx, length);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void SaveCanLogButton_Click(object sender, EventArgs e)
        {
            if (saveCanLogFileDialog.ShowDialog() == DialogResult.OK)
            {
                // If the file name is not an empty string open it for saving.
                if (saveCanLogFileDialog.FileName != "")
                {
                    using (StreamWriter sw = new StreamWriter(saveCanLogFileDialog.FileName))
                    {
                        sw.Write(canLogTextBox.Text);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
        }

        private void NodeEnableTxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Get selected Node
            Node selectedNode = (Node)RestbusNodesListBox.SelectedItem;
            // Update Node properties
            selectedNode.SetEnableTransmission(nodeEnableTxCheckBox.Checked, true);
            // Update Frame properties
            UpdateRestbusFrameProperties(sender, e);
        }

        private void NodeNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Get new value
            string name = nodeNameTextBox.Text;
            // Get selected Node
            Node selectedNode = (Node)RestbusNodesListBox.SelectedItem;
            // Update Node properties
            selectedNode.SetName(name);
        }

        private void FrameEnableTxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Get selected Frame
            Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
            //Update Frame properties
            selectedFrame.SetEnableTransmission(frameEnableTxCheckBox.Checked);
        }

        private void FrameNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Get new value
            string name = frameNameTextBox.Text;
            // Get selected Frame
            Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
            // Update Node properties
            selectedFrame.SetName(name);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void LoadDBCFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDBCFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Load database
                    Kvadblib.Status dbstatus;
                    Kvadblib.Hnd dbhandle = new Kvadblib.Hnd();

                    dbstatus = Kvadblib.Open(out dbhandle);
                    DisplayDBError(dbstatus, "Opening database handle ");
                    dbstatus = Kvadblib.ReadFile(dbhandle, openDBCFileDialog.FileName);
                    DisplayDBError(dbstatus, "Reading database from file ");

                    ReadDbNodes(dbhandle);

                    ReadDbFrames(dbhandle);

                    UpdateRestbusNodesListBox();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void DataBXNumericUpDown_ValueChanged(object sender, EventArgs e)
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

            for (Byte idx = 0; idx < Frame.FRAME_LENGTH_MAX; idx++)
            {
                if ((NumericUpDown)sender == dataBytes[idx])
                {
                    Byte value = (Byte)dataBytes[idx].Value;
                    // Get selected Frame
                    Frame selectedFrame = (Frame)RestbusFramesListBox.SelectedItem;
                    // Set selected frame data bytes
                    selectedFrame.SetData(idx, value);
                }
            }
        }

        private void SignalStartBitNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Get new value
            Byte startBit = (Byte)signalStartBitNumericUpDown.Value;
            // Get selected Signal
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            // Set selected Signal start bit
            selectedSignal.SetStartBit(startBit);
        }

        private void SignalBitLengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Get new value
            Byte bitLength = (Byte)signalBitLengthNumericUpDown.Value;
            // Get selected Signal
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            // Set selected Signal start bit
            selectedSignal.SetBitLength(bitLength);
        }

        private void SignalScaleTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get new value
                double scale = double.Parse(signalScaleTextBox.Text);
                // Get selected Signal
                Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
                // Set selected Signal scale
                selectedSignal.SetScale(scale);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse signal scale '{signalScaleTextBox.Text}'");
            }
        }

        private void SignalOffsetTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get new value
                double offset = double.Parse(signalOffsetTextBox.Text);
                // Get selected Signal
                Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
                // Set selected Signal offset
                selectedSignal.SetOffset(offset);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse signal offset '{signalOffsetTextBox.Text}'");
            }
        }

        private void SignalMinTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get new value
                double min = double.Parse(signalMinTextBox.Text);
                // Get selected Signal
                Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
                // Set selected Signal min
                selectedSignal.SetMin(min);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse signal min '{signalMinTextBox.Text}'");
            }
        }

        private void SignalMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get new value
                double max = double.Parse(signalMaxTextBox.Text);
                // Get selected Signal
                Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
                // Set selected Signal max
                selectedSignal.SetMax(max);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse signal max '{signalMaxTextBox.Text}'");
            }
        }

        private void SignalValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get new value
                decimal value = decimal.Parse(signalValueTextBox.Text);
                // Get selected Signal
                Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
                // Set selected Signal value
                selectedSignal.SetValue(value);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse signal value '{signalValueTextBox.Text}'");
            }
        }

        private void SignalRawValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get new value
                UInt64 rawValue = UInt64.Parse(signalRawValueTextBox.Text);
                // Get selected Signal
                Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
                // Set selected Signal raw value
                selectedSignal.SetRawValue(rawValue);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse signal raw value '{signalRawValueTextBox.Text}'");
            }
        }

        private void SignalUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            // Get new value
            string unit = signalUnitTextBox.Text;
            // Get selected Signal
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            // Set selected Signal unit
            selectedSignal.SetUnit(unit);
        }

        private void SignalEncodingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected encoding
            string encodingStr = signalEncodingComboBox.SelectedItem.ToString();
            Kvadblib.SignalEncoding encoding;
            if ("Intel" == encodingStr)
            {
                encoding = Kvadblib.SignalEncoding.Intel;
            }
            else
            {
                encoding = Kvadblib.SignalEncoding.Motorola;
            }
            // Get selected Signal
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            // Set selected Signal unit
            selectedSignal.SetEncoding(encoding);
        }

        private void SignalNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Get new value
            string name = signalNameTextBox.Text;
            // Get selected Signal
            Signal selectedSignal = (Signal)RestbusSignalsListBox.SelectedItem;
            // Update Node properties
            selectedSignal.SetName(name);
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            //ProcessLogLine(sender, e, false, false);
        }

        private void SendLogFrame(object sender, EventArgs e, Boolean aCheckTime = false, Boolean aSaveStartTime = true)
        {
            // Get current selection
            int selectionStart = ThreadHelper.GetSelectionStart(this, canLogTextBox);
            int selectedLineIdx = ThreadHelper.GetLineFromCharIndex(this, canLogTextBox, selectionStart);
            // Decode the line into Frame parameters
            string line = ThreadHelper.GetLine(this, canLogTextBox, selectedLineIdx);
            char[] separators = { ' ' };
            string[] splitLine = line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
            int nrOfElements = splitLine.Count();
            int canCh = int.Parse(splitLine[0]);
            int msgId = int.Parse(splitLine[1]);
            int msgDlc = int.Parse(splitLine[2]);
            byte[] data = new byte[msgDlc];
            int msgFlags = 0;
            for (int i = 0; i < msgDlc; i++)
            {
                data[i] = Byte.Parse(splitLine[i + 3]);
            }
            float time = float.Parse(splitLine[nrOfElements - 2]);
            if (aCheckTime)
            {
                // Get current time from system time

                if (aSaveStartTime)
                {
                    // Save current system time as start of run action
                    // Save time from log line as start of log run action
                }
                else
                {
                    // Check if current system time - start system time > log line time - log run time

                    // else return
                }


            }
            // Send the frame
            //Canlib.canWriteWait(canChanHdl[canCh], msgId, data, msgDlc, msgFlags, 50);
            //Canlib.canWrite(canChanHdl[canCh], msgId, data, msgDlc, msgFlags); canChanHdl
            logFrames[selectedLineIdx].SendFrame(canChanHdl);
            SelectNextLogLine();
        }

        private void ProcessLogLine(object sender, EventArgs e)
        {
            // Get current selection
            int selectionStart = ThreadHelper.GetSelectionStart(this, canLogTextBox);
            int selectedLineIdx = ThreadHelper.GetLineFromCharIndex(this, canLogTextBox, selectionStart);
            // Decode the line into Frame parameters
            //string line = ThreadHelper.GetLine(this, canLogTextBox, selectedLineIdx);
            string line = canLogTextBox.Lines[selectedLineIdx];
            if (line != "")
            {
                char[] separators = { ' ' };
                string[] splitLine = line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                int nrOfElements = splitLine.Count();
                int canCh = int.Parse(splitLine[0]);
                UInt16 msgId = UInt16.Parse(splitLine[1]);
                Byte msgDlc = Byte.Parse(splitLine[2]);
                byte[] data = new byte[msgDlc];
                int msgFlags = 0;
                double time = double.Parse(splitLine[nrOfElements - 2]);
                LogFrame lFrame = new LogFrame(msgId, "", 0, msgDlc, null, true, canCh, time);
                for (Byte i = 0; i < msgDlc; i++)
                {
                    lFrame.SetData(i, Byte.Parse(splitLine[i + 3]));
                }

                // Save the frame in processed LogFrames
                // TODO: Save also the line index, and selection index and length for faster processing
                logFrames.Add(lFrame);
            }
            
            SelectNextLogLine();
        }

        private void SelectNextLogLine()
        {
            // Get current selection
            int selectionStart = ThreadHelper.GetSelectionStart(this, canLogTextBox);
            int selectedLineIdx = ThreadHelper.GetLineFromCharIndex(this, canLogTextBox, selectionStart);
            int linesCount = ThreadHelper.GetLinesCount(this, canLogTextBox);

            // Check if the next line exists
            if ((selectedLineIdx + 1) < linesCount)
            {
                // Create new selection
                selectedLineIdx += 1;
                selectionStart = ThreadHelper.GetFirstCharIndexFromLine(this, canLogTextBox, selectedLineIdx);
                //string line = ThreadHelper.GetLine(this, canLogTextBox, selectedLineIdx);
                string line = canLogTextBox.Lines[selectedLineIdx];
                int length = line.Length;
                ThreadHelper.Select(this, canLogTextBox, selectionStart, length);
            }
        }

        private void KoenigseggHWTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            Canlib.canStatus status;

            for (int ch = 0; ch < CAN_CHANNEL_NR; ch++)
            {
                //Go off bus and close channel
                status = Canlib.canBusOff(canChanHdl[ch]);
                DisplayError(status, "canBusOff");

                status = Canlib.canClose(canChanHdl[ch]);
                DisplayError(status, "canClose");
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (logRunBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                logRunBackgroundWorker.RunWorkerAsync();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (true == logRunBackgroundWorker.WorkerSupportsCancellation)
            {
                // Cancel the asynchronous operation.
                logRunBackgroundWorker.CancelAsync();
                logVerifyBackgroundWorker.CancelAsync();
            }
        }

        private void LogRunBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Boolean firstCall = true;
            int linesCount = 0;

            while (true)
            {
                if (true == worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (firstCall)
                    {
                        // Get total number of lines
                        linesCount = ThreadHelper.GetLinesCount(this, canLogTextBox);
                    }
                    SendLogFrame(sender, e, true, firstCall);
                    // Get selected line number
                    int selectionStart = ThreadHelper.GetSelectionStart(this, canLogTextBox);
                    int selectedLineIdx = ThreadHelper.GetLineFromCharIndex(this, canLogTextBox, selectionStart);
                    // Calculate progress in percent
                    int progressPercent = (int)((float)selectedLineIdx / (linesCount - 1)* 100);
                    worker.ReportProgress(progressPercent);
                    if (selectedLineIdx == (linesCount - 1))
                    {
                        break;
                    }
                }
                firstCall = false;
            }
        }

        private void LogRunBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Display progress
            logProgressBar.Value = e.ProgressPercentage;
            logProgressLabel.Text = $"Running... {e.ProgressPercentage.ToString()}%";
        }

        private void LogRunBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (true == e.Cancelled)
            {
                // Do nothing
            }
            else if (e.Error != null)
            {
                // Report some error?
                //resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Select first line?
            }
        }

        private void LogVerifyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Boolean firstCall = true;
            int linesCount = 0;

            while (true)
            {
                if (true == worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (firstCall)
                    {
                        // Get total number of lines
                        linesCount = ThreadHelper.GetLinesCount(this, canLogTextBox);
                    }
                    ProcessLogLine(sender, e);
                    // Get selected line number
                    int selectionStart = ThreadHelper.GetSelectionStart(this, canLogTextBox);
                    int selectedLineIdx = ThreadHelper.GetLineFromCharIndex(this, canLogTextBox, selectionStart);
                    // Calculate progress in percent
                    int progressPercent = (int)((float)selectedLineIdx / (linesCount - 1) * 100);
                    worker.ReportProgress(progressPercent);
                    if (selectedLineIdx == (linesCount - 1))
                    {
                        break;
                    }
                }
            }
        }

        private void LogVerifyBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Display progress
            logProgressBar.Value = e.ProgressPercentage;
            logProgressLabel.Text = $"Verifying... {e.ProgressPercentage.ToString()}%";
        }

        private void LogVerifyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (true == e.Cancelled)
            {
                // Do nothing
            }
            else if (e.Error != null)
            {
                // Report some error?
                //resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Select first line
                int idx = canLogTextBox.GetFirstCharIndexFromLine(0);
                string line = canLogTextBox.Lines[0];
                int length = line.Length;
                canLogTextBox.Select(idx, length);
            }
        }

        private void verifyCanLogButton_Click(object sender, EventArgs e)
        {
            if (logVerifyBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                logVerifyBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}