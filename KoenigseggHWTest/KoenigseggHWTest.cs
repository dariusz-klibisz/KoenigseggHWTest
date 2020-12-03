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
        private TestFrame CANFrame = new TestFrame(aFrameID: DEFAULT_FRAME_ID);
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
            if(true == frameIdCheckBox.Checked)
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
                CANFrame.SetFrameID(idInt);
                Debug.Print(CANFrame.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{frameIDNumericUpDown.Value}'");
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
                CANFrame.SetFramePeriod(framePeriodInt);
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
            Kvadblib.Hnd dbhandle;
            Kvadblib.Status dbstatus;

            string filename = "C:\\Repos\\KoenigseggHWTest\\Regera_HSCAN1.dbc";
            dbhandle = new Kvadblib.Hnd();
            dbstatus = Kvadblib.Open(out dbhandle);
            DisplayDBError(dbstatus, "Opening database handle ");
            dbstatus = Kvadblib.ReadFile(dbhandle, filename);
            DisplayDBError(dbstatus, "Reading database from file ");

            ReadDbNodes(dbhandle);

            ReadDbFrames(dbhandle);

            

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

        private static void ReadDbNodes(Kvadblib.Hnd aDbHnd)
        {
            Kvadblib.NodeHnd nh = new Kvadblib.NodeHnd();
            string name = "";

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
            nodes.Add(new Node(name));
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
            Kvadblib.MESSAGE frameFlags;

            Kvadblib.GetMsgName(aMsgHnd, out name);
            Kvadblib.GetMsgId(aMsgHnd, out frameID, out frameFlags);
            Kvadblib.GetMsgSendNode(aMsgHnd, out nh);
            Kvadblib.GetNodeName(nh, out senderNode);
            Debug.Print(name);
            foreach (Node node in nodes)
            {
                if (node.GetNodeName() == senderNode)
                {
                    node.AddFrame(new Frame((UInt16)frameID, name));
                }
            }
            ReadDbSignals(aMsgHnd);
        }

        private static void ReadDbSignals(Kvadblib.MessageHnd aMsgHnd)
        {
            Kvadblib.SignalHnd sh = new Kvadblib.SignalHnd();

            if (Kvadblib.Status.OK == Kvadblib.GetFirstSignal(aMsgHnd, out sh))
            {
                ReadSignal(sh);
            }

            while (Kvadblib.Status.OK == Kvadblib.GetNextSignal(aMsgHnd, out sh))
            {
                ReadSignal(sh);
            }            
        }

        private static void ReadSignal(Kvadblib.SignalHnd aSgnHnd)
        {
            string name = "";
            string receiverNode = "";
            int size = 0;
            int startBit = 0;
            double factor = 0.0;
            double offset = 0.0;
            double min = 0.0;
            double max = 0.0;
            string unit = "";
            Kvadblib.AttributeHnd ah = new Kvadblib.AttributeHnd();
            Kvaser.Kvadblib.Kvadblib.NodeHnd nh;
            Kvadblib.SignalType pt;
            Kvadblib.SignalType rt;

            Kvadblib.GetSignalName(aSgnHnd, out name);
            Kvadblib.GetSignalValueSize(aSgnHnd, out startBit, out size);
            Kvadblib.GetSignalValueScaling(aSgnHnd, out factor, out offset);
            Kvadblib.GetSignalValueLimits(aSgnHnd, out min, out max);
            Kvadblib.GetSignalUnit(aSgnHnd, out unit);
            Kvadblib.GetSignalPresentationType(aSgnHnd, out pt);
            Kvadblib.GetSignalRepresentationType(aSgnHnd, out rt);
            //TODO: Fix this receiverNode
            //Kvadblib.SignalContainsReceiveNode(aSgnHnd, out nh);


            //Kvadblib.GetFirstSignalAttribute(sh, ref ah);
            //Kvadblib.GetAttributeName(ah, out name);
            Debug.Print(name);
        }
    }
}