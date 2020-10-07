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
        private const UInt16 defaultFrameID = 2016;

        Frame CANFrame = new Frame(aFrameID: defaultFrameID);
        public KoenigseggHWTest()
        {
            InitializeComponent();
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

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton24_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
