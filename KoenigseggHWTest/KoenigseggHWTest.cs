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

namespace KoenigseggHWTest
{
    public partial class KoenigseggHWTest : Form
    {
        private const UInt16 DEFAULT_FRAME_ID = 2016;
        private Frame CANFrame = new Frame(aFrameID: DEFAULT_FRAME_ID);

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
    }
}
