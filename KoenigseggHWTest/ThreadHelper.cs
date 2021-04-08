using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoenigseggHWTest
{
    class ThreadHelper
    {
        delegate void SetTextCallback(Form aForm, Control aCtrl, string aText);
        delegate int GetSelectionStartCallback(Form aForm, TextBox aCtrl);
        delegate int GetLineFromCharIndexCallback(Form aForm, TextBox aCtrl, int aIdx);
        delegate int GetLinesCountCallback(Form aForm, TextBox aCtrl);
        delegate string GetLineCallback(Form aForm, TextBox aCtrl, int aIdx);
        delegate int GetFirstCharIndexFromLineCallback(Form aForm, TextBox aCtrl, int aIdx);
        delegate void SelectCallback(Form aForm, TextBox aCtrl, int aStart, int aLength);

        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control for which text will be set</param>
        /// <param name="aText">Text to set</param>
        public static void SetText(Form aForm, Control aCtrl, string aText)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                aForm.Invoke(d, new object[] { aForm, aCtrl, aText });
            }
            else
            {
                aCtrl.Text = aText;
            }
        }

        /// <summary>
        /// Get SelectionStart property of various controls (textbox)
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control from which property value will be returned</param>
        public static int GetSelectionStart(Form aForm, TextBox aCtrl)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                GetSelectionStartCallback d = new GetSelectionStartCallback(GetSelectionStart);
                return (int)aForm.Invoke(d, new object[] { aForm, aCtrl });
            }
            else
            {
                return aCtrl.SelectionStart;
            }
        }

        /// <summary>
        /// Get line index from character position of various controls (textbox)
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control from which the value will be returned</param>
        /// <param name="aIdx">Character position for which line index will be returned</param>
        public static int GetLineFromCharIndex(Form aForm, TextBox aCtrl, int aIdx)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                GetLineFromCharIndexCallback d = new GetLineFromCharIndexCallback(GetLineFromCharIndex);
                return (int)aForm.Invoke(d, new object[] { aForm, aCtrl, aIdx });
            }
            else
            {
                return aCtrl.GetLineFromCharIndex(aIdx);
            }
        }

        /// <summary>
        /// Get number of lines of various controls (textbox)
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control from which property value will be returned</param>
        public static int GetLinesCount(Form aForm, TextBox aCtrl)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                GetLinesCountCallback d = new GetLinesCountCallback(GetLinesCount);
                return (int)aForm.Invoke(d, new object[] { aForm, aCtrl });
            }
            else
            {
                return aCtrl.Lines.Count();
            }
        }

        /// <summary>
        /// Get line of various controls (textbox)
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control from which the value will be returned</param>
        /// <param name="aIdx">Index of line which will be returned</param>
        public static string GetLine(Form aForm, TextBox aCtrl, int aIdx)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                GetLineCallback d = new GetLineCallback(GetLine);
                return (string)aForm.Invoke(d, new object[] { aForm, aCtrl, aIdx });
            }
            else
            {
                return aCtrl.Lines[aIdx];
            }
        }

        /// <summary>
        /// Get index of first character in line of various controls (textbox)
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control from which the value will be returned</param>
        /// <param name="aIdx">Index of line which first character index will be returned</param>
        public static int GetFirstCharIndexFromLine(Form aForm, TextBox aCtrl, int aIdx)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                GetFirstCharIndexFromLineCallback d = new GetFirstCharIndexFromLineCallback(GetFirstCharIndexFromLine);
                return (int)aForm.Invoke(d, new object[] { aForm, aCtrl, aIdx });
            }
            else
            {
                return aCtrl.GetFirstCharIndexFromLine(aIdx);
            }
        }

        /// <summary>
        /// Selects text in line of various controls (textbox)
        /// </summary>
        /// <param name="aForm">The calling form</param>
        /// <param name="aCtrl">Control for which the selection will be made</param>
        /// <param name="aStart">Index of start of selection</param>
        /// <param name="aLength">Length of selection</param>
        public static void Select(Form aForm, TextBox aCtrl, int aStart, int aLength)
        {
            // InvokeRequired compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (aCtrl.InvokeRequired)
            {
                SelectCallback d = new SelectCallback(Select);
                aForm.Invoke(d, new object[] { aForm, aCtrl, aStart, aLength });
            }
            else
            {
                aCtrl.Select(aStart, aLength);
            }
        }
    }
}
