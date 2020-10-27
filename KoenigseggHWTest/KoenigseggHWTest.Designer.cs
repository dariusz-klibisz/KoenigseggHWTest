namespace KoenigseggHWTest
{
    partial class KoenigseggHWTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.framePropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.frameIdTextBox = new System.Windows.Forms.TextBox();
            this.frameIdCheckBox = new System.Windows.Forms.CheckBox();
            this.frameIDNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.framePeriodLabel = new System.Windows.Forms.Label();
            this.framePeriodNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.frameIDLabel = new System.Windows.Forms.Label();
            this.frameDataGroupBox = new System.Windows.Forms.GroupBox();
            this.pullSelectGroupBox = new System.Windows.Forms.GroupBox();
            this.pullUpRadioButton = new System.Windows.Forms.RadioButton();
            this.pulldownRadioButton = new System.Windows.Forms.RadioButton();
            this.pullControlGroupBox = new System.Windows.Forms.GroupBox();
            this.pullEnableRadioButton = new System.Windows.Forms.RadioButton();
            this.pullDisableRadioButton = new System.Windows.Forms.RadioButton();
            this.slewRateControlGroupBox = new System.Windows.Forms.GroupBox();
            this.fullWoSlewCtrlRadioButton = new System.Windows.Forms.RadioButton();
            this.halfWoSlewCtrlRadioButton = new System.Windows.Forms.RadioButton();
            this.fullWSlewCtrlRadioButton = new System.Windows.Forms.RadioButton();
            this.halfWSlewCtrlRadioButton = new System.Windows.Forms.RadioButton();
            this.inputHysteresisGroupBox = new System.Windows.Forms.GroupBox();
            this.inputHysteresisEnableRadioButton = new System.Windows.Forms.RadioButton();
            this.inputHysteresisDisableRadioButton = new System.Windows.Forms.RadioButton();
            this.openDrainGroupBox = new System.Windows.Forms.GroupBox();
            this.openDrainEnableRadioButton = new System.Windows.Forms.RadioButton();
            this.openDrainDisableRadioButton = new System.Windows.Forms.RadioButton();
            this.driveStrGroupBox = new System.Windows.Forms.GroupBox();
            this.maximumRadioButton = new System.Windows.Forms.RadioButton();
            this.mediumHighRadioButton = new System.Windows.Forms.RadioButton();
            this.mediumRadioButton = new System.Windows.Forms.RadioButton();
            this.minimumRadioButton = new System.Windows.Forms.RadioButton();
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.inputEnableRadioButton = new System.Windows.Forms.RadioButton();
            this.inputDisableRadioButton = new System.Windows.Forms.RadioButton();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.outputEnableRadioButton = new System.Windows.Forms.RadioButton();
            this.outputDisableRadioButton = new System.Windows.Forms.RadioButton();
            this.functionGroupBox = new System.Windows.Forms.GroupBox();
            this.alternate3RadioButton = new System.Windows.Forms.RadioButton();
            this.alternate2RadioButton = new System.Windows.Forms.RadioButton();
            this.alternate1RadioButton = new System.Windows.Forms.RadioButton();
            this.primaryRadioButton = new System.Windows.Forms.RadioButton();
            this.gpioRadioButton = new System.Windows.Forms.RadioButton();
            this.udsRoutineLabel = new System.Windows.Forms.Label();
            this.udsRoutineNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pinNumberLabel = new System.Windows.Forms.Label();
            this.pinNumberComboBox = new System.Windows.Forms.ComboBox();
            this.transmissionControlGroupBox = new System.Windows.Forms.GroupBox();
            this.startTransmisionButton = new System.Windows.Forms.Button();
            this.framePropertiesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameIDNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.framePeriodNumericUpDown)).BeginInit();
            this.frameDataGroupBox.SuspendLayout();
            this.pullSelectGroupBox.SuspendLayout();
            this.pullControlGroupBox.SuspendLayout();
            this.slewRateControlGroupBox.SuspendLayout();
            this.inputHysteresisGroupBox.SuspendLayout();
            this.openDrainGroupBox.SuspendLayout();
            this.driveStrGroupBox.SuspendLayout();
            this.inputGroupBox.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.functionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udsRoutineNumericUpDown)).BeginInit();
            this.transmissionControlGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // framePropertiesGroupBox
            // 
            this.framePropertiesGroupBox.Controls.Add(this.frameIdTextBox);
            this.framePropertiesGroupBox.Controls.Add(this.frameIdCheckBox);
            this.framePropertiesGroupBox.Controls.Add(this.frameIDNumericUpDown);
            this.framePropertiesGroupBox.Controls.Add(this.framePeriodLabel);
            this.framePropertiesGroupBox.Controls.Add(this.framePeriodNumericUpDown);
            this.framePropertiesGroupBox.Controls.Add(this.frameIDLabel);
            this.framePropertiesGroupBox.Location = new System.Drawing.Point(44, 536);
            this.framePropertiesGroupBox.Name = "framePropertiesGroupBox";
            this.framePropertiesGroupBox.Size = new System.Drawing.Size(522, 308);
            this.framePropertiesGroupBox.TabIndex = 0;
            this.framePropertiesGroupBox.TabStop = false;
            this.framePropertiesGroupBox.Text = "Frame Properties";
            // 
            // frameIdTextBox
            // 
            this.frameIdTextBox.Location = new System.Drawing.Point(58, 104);
            this.frameIdTextBox.Name = "frameIdTextBox";
            this.frameIdTextBox.ReadOnly = true;
            this.frameIdTextBox.Size = new System.Drawing.Size(37, 38);
            this.frameIdTextBox.TabIndex = 10;
            this.frameIdTextBox.Text = "0x";
            this.frameIdTextBox.Visible = false;
            // 
            // frameIdCheckBox
            // 
            this.frameIdCheckBox.AutoSize = true;
            this.frameIdCheckBox.Location = new System.Drawing.Point(259, 106);
            this.frameIdCheckBox.Name = "frameIdCheckBox";
            this.frameIdCheckBox.Size = new System.Drawing.Size(103, 36);
            this.frameIdCheckBox.TabIndex = 8;
            this.frameIdCheckBox.Text = "Hex";
            this.frameIdCheckBox.UseVisualStyleBackColor = true;
            this.frameIdCheckBox.CheckedChanged += new System.EventHandler(this.frameIdCheckBox_CheckedChanged);
            // 
            // frameIDNumericUpDown
            // 
            this.frameIDNumericUpDown.Location = new System.Drawing.Point(101, 104);
            this.frameIDNumericUpDown.Maximum = new decimal(new int[] {
            2047,
            0,
            0,
            0});
            this.frameIDNumericUpDown.Name = "frameIDNumericUpDown";
            this.frameIDNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.frameIDNumericUpDown.TabIndex = 7;
            this.frameIDNumericUpDown.ValueChanged += new System.EventHandler(this.frameIDNumericUpDown_ValueChanged);
            // 
            // framePeriodLabel
            // 
            this.framePeriodLabel.AutoSize = true;
            this.framePeriodLabel.Location = new System.Drawing.Point(36, 171);
            this.framePeriodLabel.Name = "framePeriodLabel";
            this.framePeriodLabel.Size = new System.Drawing.Size(243, 32);
            this.framePeriodLabel.TabIndex = 6;
            this.framePeriodLabel.Text = "Frame period [ms]";
            // 
            // framePeriodNumericUpDown
            // 
            this.framePeriodNumericUpDown.Location = new System.Drawing.Point(42, 222);
            this.framePeriodNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.framePeriodNumericUpDown.Name = "framePeriodNumericUpDown";
            this.framePeriodNumericUpDown.Size = new System.Drawing.Size(243, 38);
            this.framePeriodNumericUpDown.TabIndex = 5;
            this.framePeriodNumericUpDown.ValueChanged += new System.EventHandler(this.framePeriodNumericUpDown_ValueChanged);
            // 
            // frameIDLabel
            // 
            this.frameIDLabel.AutoSize = true;
            this.frameIDLabel.Location = new System.Drawing.Point(95, 55);
            this.frameIDLabel.Name = "frameIDLabel";
            this.frameIDLabel.Size = new System.Drawing.Size(130, 32);
            this.frameIDLabel.TabIndex = 2;
            this.frameIDLabel.Text = "Frame ID";
            // 
            // frameDataGroupBox
            // 
            this.frameDataGroupBox.Controls.Add(this.pullSelectGroupBox);
            this.frameDataGroupBox.Controls.Add(this.pullControlGroupBox);
            this.frameDataGroupBox.Controls.Add(this.slewRateControlGroupBox);
            this.frameDataGroupBox.Controls.Add(this.inputHysteresisGroupBox);
            this.frameDataGroupBox.Controls.Add(this.openDrainGroupBox);
            this.frameDataGroupBox.Controls.Add(this.driveStrGroupBox);
            this.frameDataGroupBox.Controls.Add(this.inputGroupBox);
            this.frameDataGroupBox.Controls.Add(this.outputGroupBox);
            this.frameDataGroupBox.Controls.Add(this.functionGroupBox);
            this.frameDataGroupBox.Controls.Add(this.udsRoutineLabel);
            this.frameDataGroupBox.Controls.Add(this.udsRoutineNumericUpDown);
            this.frameDataGroupBox.Controls.Add(this.pinNumberLabel);
            this.frameDataGroupBox.Controls.Add(this.pinNumberComboBox);
            this.frameDataGroupBox.Location = new System.Drawing.Point(50, 51);
            this.frameDataGroupBox.Name = "frameDataGroupBox";
            this.frameDataGroupBox.Size = new System.Drawing.Size(2573, 454);
            this.frameDataGroupBox.TabIndex = 1;
            this.frameDataGroupBox.TabStop = false;
            this.frameDataGroupBox.Text = "Frame Data";
            // 
            // pullSelectGroupBox
            // 
            this.pullSelectGroupBox.Controls.Add(this.pullUpRadioButton);
            this.pullSelectGroupBox.Controls.Add(this.pulldownRadioButton);
            this.pullSelectGroupBox.Location = new System.Drawing.Point(2019, 237);
            this.pullSelectGroupBox.Name = "pullSelectGroupBox";
            this.pullSelectGroupBox.Size = new System.Drawing.Size(326, 180);
            this.pullSelectGroupBox.TabIndex = 8;
            this.pullSelectGroupBox.TabStop = false;
            this.pullSelectGroupBox.Text = "Weak Pull-up/down Select";
            // 
            // pullUpRadioButton
            // 
            this.pullUpRadioButton.AutoSize = true;
            this.pullUpRadioButton.Location = new System.Drawing.Point(7, 112);
            this.pullUpRadioButton.Name = "pullUpRadioButton";
            this.pullUpRadioButton.Size = new System.Drawing.Size(142, 36);
            this.pullUpRadioButton.TabIndex = 1;
            this.pullUpRadioButton.Text = "Pull-up";
            this.pullUpRadioButton.UseVisualStyleBackColor = true;
            this.pullUpRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // pulldownRadioButton
            // 
            this.pulldownRadioButton.AutoSize = true;
            this.pulldownRadioButton.Checked = true;
            this.pulldownRadioButton.Location = new System.Drawing.Point(7, 69);
            this.pulldownRadioButton.Name = "pulldownRadioButton";
            this.pulldownRadioButton.Size = new System.Drawing.Size(178, 36);
            this.pulldownRadioButton.TabIndex = 0;
            this.pulldownRadioButton.TabStop = true;
            this.pulldownRadioButton.Text = "Pull-down";
            this.pulldownRadioButton.UseVisualStyleBackColor = true;
            this.pulldownRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // pullControlGroupBox
            // 
            this.pullControlGroupBox.Controls.Add(this.pullEnableRadioButton);
            this.pullControlGroupBox.Controls.Add(this.pullDisableRadioButton);
            this.pullControlGroupBox.Location = new System.Drawing.Point(2019, 48);
            this.pullControlGroupBox.Name = "pullControlGroupBox";
            this.pullControlGroupBox.Size = new System.Drawing.Size(326, 165);
            this.pullControlGroupBox.TabIndex = 7;
            this.pullControlGroupBox.TabStop = false;
            this.pullControlGroupBox.Text = "Weak Pull-up/down Control";
            // 
            // pullEnableRadioButton
            // 
            this.pullEnableRadioButton.AutoSize = true;
            this.pullEnableRadioButton.Location = new System.Drawing.Point(20, 116);
            this.pullEnableRadioButton.Name = "pullEnableRadioButton";
            this.pullEnableRadioButton.Size = new System.Drawing.Size(142, 36);
            this.pullEnableRadioButton.TabIndex = 1;
            this.pullEnableRadioButton.Text = "Enable";
            this.pullEnableRadioButton.UseVisualStyleBackColor = true;
            this.pullEnableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // pullDisableRadioButton
            // 
            this.pullDisableRadioButton.AutoSize = true;
            this.pullDisableRadioButton.Checked = true;
            this.pullDisableRadioButton.Location = new System.Drawing.Point(20, 64);
            this.pullDisableRadioButton.Name = "pullDisableRadioButton";
            this.pullDisableRadioButton.Size = new System.Drawing.Size(148, 36);
            this.pullDisableRadioButton.TabIndex = 0;
            this.pullDisableRadioButton.TabStop = true;
            this.pullDisableRadioButton.Text = "Disable";
            this.pullDisableRadioButton.UseVisualStyleBackColor = true;
            this.pullDisableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // slewRateControlGroupBox
            // 
            this.slewRateControlGroupBox.Controls.Add(this.fullWoSlewCtrlRadioButton);
            this.slewRateControlGroupBox.Controls.Add(this.halfWoSlewCtrlRadioButton);
            this.slewRateControlGroupBox.Controls.Add(this.fullWSlewCtrlRadioButton);
            this.slewRateControlGroupBox.Controls.Add(this.halfWSlewCtrlRadioButton);
            this.slewRateControlGroupBox.Location = new System.Drawing.Point(1397, 38);
            this.slewRateControlGroupBox.Name = "slewRateControlGroupBox";
            this.slewRateControlGroupBox.Size = new System.Drawing.Size(567, 383);
            this.slewRateControlGroupBox.TabIndex = 6;
            this.slewRateControlGroupBox.TabStop = false;
            this.slewRateControlGroupBox.Text = "Slew Rate Control";
            // 
            // fullWoSlewCtrlRadioButton
            // 
            this.fullWoSlewCtrlRadioButton.AutoSize = true;
            this.fullWoSlewCtrlRadioButton.Location = new System.Drawing.Point(7, 268);
            this.fullWoSlewCtrlRadioButton.Name = "fullWoSlewCtrlRadioButton";
            this.fullWoSlewCtrlRadioButton.Size = new System.Drawing.Size(559, 36);
            this.fullWoSlewCtrlRadioButton.TabIndex = 3;
            this.fullWoSlewCtrlRadioButton.Text = "Full Drive Strength Without Slew Control";
            this.fullWoSlewCtrlRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fullWoSlewCtrlRadioButton.UseVisualStyleBackColor = true;
            this.fullWoSlewCtrlRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // halfWoSlewCtrlRadioButton
            // 
            this.halfWoSlewCtrlRadioButton.AutoSize = true;
            this.halfWoSlewCtrlRadioButton.Location = new System.Drawing.Point(7, 186);
            this.halfWoSlewCtrlRadioButton.Name = "halfWoSlewCtrlRadioButton";
            this.halfWoSlewCtrlRadioButton.Size = new System.Drawing.Size(563, 36);
            this.halfWoSlewCtrlRadioButton.TabIndex = 2;
            this.halfWoSlewCtrlRadioButton.Text = "Half Drive Strength Without Slew Control";
            this.halfWoSlewCtrlRadioButton.UseVisualStyleBackColor = true;
            this.halfWoSlewCtrlRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // fullWSlewCtrlRadioButton
            // 
            this.fullWSlewCtrlRadioButton.AutoSize = true;
            this.fullWSlewCtrlRadioButton.Location = new System.Drawing.Point(7, 114);
            this.fullWSlewCtrlRadioButton.Name = "fullWSlewCtrlRadioButton";
            this.fullWSlewCtrlRadioButton.Size = new System.Drawing.Size(519, 36);
            this.fullWSlewCtrlRadioButton.TabIndex = 1;
            this.fullWSlewCtrlRadioButton.Text = "Full Drive Strength With Slew Control";
            this.fullWSlewCtrlRadioButton.UseVisualStyleBackColor = true;
            this.fullWSlewCtrlRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // halfWSlewCtrlRadioButton
            // 
            this.halfWSlewCtrlRadioButton.AutoSize = true;
            this.halfWSlewCtrlRadioButton.Checked = true;
            this.halfWSlewCtrlRadioButton.Location = new System.Drawing.Point(6, 62);
            this.halfWSlewCtrlRadioButton.Name = "halfWSlewCtrlRadioButton";
            this.halfWSlewCtrlRadioButton.Size = new System.Drawing.Size(530, 36);
            this.halfWSlewCtrlRadioButton.TabIndex = 0;
            this.halfWSlewCtrlRadioButton.TabStop = true;
            this.halfWSlewCtrlRadioButton.Text = "Half Drive Strength With Slew Control ";
            this.halfWSlewCtrlRadioButton.UseVisualStyleBackColor = true;
            this.halfWSlewCtrlRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // inputHysteresisGroupBox
            // 
            this.inputHysteresisGroupBox.Controls.Add(this.inputHysteresisEnableRadioButton);
            this.inputHysteresisGroupBox.Controls.Add(this.inputHysteresisDisableRadioButton);
            this.inputHysteresisGroupBox.Location = new System.Drawing.Point(1100, 237);
            this.inputHysteresisGroupBox.Name = "inputHysteresisGroupBox";
            this.inputHysteresisGroupBox.Size = new System.Drawing.Size(233, 180);
            this.inputHysteresisGroupBox.TabIndex = 5;
            this.inputHysteresisGroupBox.TabStop = false;
            this.inputHysteresisGroupBox.Text = "Input Hysteresis";
            // 
            // inputHysteresisEnableRadioButton
            // 
            this.inputHysteresisEnableRadioButton.AutoSize = true;
            this.inputHysteresisEnableRadioButton.Location = new System.Drawing.Point(17, 102);
            this.inputHysteresisEnableRadioButton.Name = "inputHysteresisEnableRadioButton";
            this.inputHysteresisEnableRadioButton.Size = new System.Drawing.Size(142, 36);
            this.inputHysteresisEnableRadioButton.TabIndex = 1;
            this.inputHysteresisEnableRadioButton.Text = "Enable";
            this.inputHysteresisEnableRadioButton.UseVisualStyleBackColor = true;
            this.inputHysteresisEnableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // inputHysteresisDisableRadioButton
            // 
            this.inputHysteresisDisableRadioButton.AutoSize = true;
            this.inputHysteresisDisableRadioButton.Checked = true;
            this.inputHysteresisDisableRadioButton.Location = new System.Drawing.Point(17, 50);
            this.inputHysteresisDisableRadioButton.Name = "inputHysteresisDisableRadioButton";
            this.inputHysteresisDisableRadioButton.Size = new System.Drawing.Size(148, 36);
            this.inputHysteresisDisableRadioButton.TabIndex = 0;
            this.inputHysteresisDisableRadioButton.TabStop = true;
            this.inputHysteresisDisableRadioButton.Text = "Disable";
            this.inputHysteresisDisableRadioButton.UseVisualStyleBackColor = true;
            this.inputHysteresisDisableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // openDrainGroupBox
            // 
            this.openDrainGroupBox.Controls.Add(this.openDrainEnableRadioButton);
            this.openDrainGroupBox.Controls.Add(this.openDrainDisableRadioButton);
            this.openDrainGroupBox.Location = new System.Drawing.Point(1093, 62);
            this.openDrainGroupBox.Name = "openDrainGroupBox";
            this.openDrainGroupBox.Size = new System.Drawing.Size(270, 151);
            this.openDrainGroupBox.TabIndex = 4;
            this.openDrainGroupBox.TabStop = false;
            this.openDrainGroupBox.Text = "Open Drain Output";
            // 
            // openDrainEnableRadioButton
            // 
            this.openDrainEnableRadioButton.AutoSize = true;
            this.openDrainEnableRadioButton.Location = new System.Drawing.Point(7, 103);
            this.openDrainEnableRadioButton.Name = "openDrainEnableRadioButton";
            this.openDrainEnableRadioButton.Size = new System.Drawing.Size(142, 36);
            this.openDrainEnableRadioButton.TabIndex = 1;
            this.openDrainEnableRadioButton.Text = "Enable";
            this.openDrainEnableRadioButton.UseVisualStyleBackColor = true;
            this.openDrainEnableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // openDrainDisableRadioButton
            // 
            this.openDrainDisableRadioButton.AutoSize = true;
            this.openDrainDisableRadioButton.Checked = true;
            this.openDrainDisableRadioButton.Location = new System.Drawing.Point(7, 50);
            this.openDrainDisableRadioButton.Name = "openDrainDisableRadioButton";
            this.openDrainDisableRadioButton.Size = new System.Drawing.Size(148, 36);
            this.openDrainDisableRadioButton.TabIndex = 0;
            this.openDrainDisableRadioButton.TabStop = true;
            this.openDrainDisableRadioButton.Text = "Disable";
            this.openDrainDisableRadioButton.UseVisualStyleBackColor = true;
            this.openDrainDisableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // driveStrGroupBox
            // 
            this.driveStrGroupBox.Controls.Add(this.maximumRadioButton);
            this.driveStrGroupBox.Controls.Add(this.mediumHighRadioButton);
            this.driveStrGroupBox.Controls.Add(this.mediumRadioButton);
            this.driveStrGroupBox.Controls.Add(this.minimumRadioButton);
            this.driveStrGroupBox.Location = new System.Drawing.Point(823, 51);
            this.driveStrGroupBox.Name = "driveStrGroupBox";
            this.driveStrGroupBox.Size = new System.Drawing.Size(243, 370);
            this.driveStrGroupBox.TabIndex = 3;
            this.driveStrGroupBox.TabStop = false;
            this.driveStrGroupBox.Text = "Drive Strength";
            // 
            // maximumRadioButton
            // 
            this.maximumRadioButton.AutoSize = true;
            this.maximumRadioButton.Location = new System.Drawing.Point(7, 217);
            this.maximumRadioButton.Name = "maximumRadioButton";
            this.maximumRadioButton.Size = new System.Drawing.Size(174, 36);
            this.maximumRadioButton.TabIndex = 3;
            this.maximumRadioButton.Text = "Maximum";
            this.maximumRadioButton.UseVisualStyleBackColor = true;
            this.maximumRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // mediumHighRadioButton
            // 
            this.mediumHighRadioButton.AutoSize = true;
            this.mediumHighRadioButton.Location = new System.Drawing.Point(7, 157);
            this.mediumHighRadioButton.Name = "mediumHighRadioButton";
            this.mediumHighRadioButton.Size = new System.Drawing.Size(217, 36);
            this.mediumHighRadioButton.TabIndex = 2;
            this.mediumHighRadioButton.Text = "Medium-high";
            this.mediumHighRadioButton.UseVisualStyleBackColor = true;
            this.mediumHighRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // mediumRadioButton
            // 
            this.mediumRadioButton.AutoSize = true;
            this.mediumRadioButton.Location = new System.Drawing.Point(7, 104);
            this.mediumRadioButton.Name = "mediumRadioButton";
            this.mediumRadioButton.Size = new System.Drawing.Size(153, 36);
            this.mediumRadioButton.TabIndex = 1;
            this.mediumRadioButton.Text = "Medium";
            this.mediumRadioButton.UseVisualStyleBackColor = true;
            this.mediumRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // minimumRadioButton
            // 
            this.minimumRadioButton.AutoSize = true;
            this.minimumRadioButton.Checked = true;
            this.minimumRadioButton.Location = new System.Drawing.Point(7, 50);
            this.minimumRadioButton.Name = "minimumRadioButton";
            this.minimumRadioButton.Size = new System.Drawing.Size(167, 36);
            this.minimumRadioButton.TabIndex = 0;
            this.minimumRadioButton.TabStop = true;
            this.minimumRadioButton.Text = "Minimum";
            this.minimumRadioButton.UseVisualStyleBackColor = true;
            this.minimumRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.Controls.Add(this.inputEnableRadioButton);
            this.inputGroupBox.Controls.Add(this.inputDisableRadioButton);
            this.inputGroupBox.Location = new System.Drawing.Point(580, 237);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(216, 193);
            this.inputGroupBox.TabIndex = 2;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Input Buffer";
            // 
            // inputEnableRadioButton
            // 
            this.inputEnableRadioButton.AutoSize = true;
            this.inputEnableRadioButton.Location = new System.Drawing.Point(7, 126);
            this.inputEnableRadioButton.Name = "inputEnableRadioButton";
            this.inputEnableRadioButton.Size = new System.Drawing.Size(142, 36);
            this.inputEnableRadioButton.TabIndex = 1;
            this.inputEnableRadioButton.Text = "Enable";
            this.inputEnableRadioButton.UseVisualStyleBackColor = true;
            this.inputEnableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // inputDisableRadioButton
            // 
            this.inputDisableRadioButton.AutoSize = true;
            this.inputDisableRadioButton.Checked = true;
            this.inputDisableRadioButton.Location = new System.Drawing.Point(7, 50);
            this.inputDisableRadioButton.Name = "inputDisableRadioButton";
            this.inputDisableRadioButton.Size = new System.Drawing.Size(148, 36);
            this.inputDisableRadioButton.TabIndex = 0;
            this.inputDisableRadioButton.TabStop = true;
            this.inputDisableRadioButton.Text = "Disable";
            this.inputDisableRadioButton.UseVisualStyleBackColor = true;
            this.inputDisableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Controls.Add(this.outputEnableRadioButton);
            this.outputGroupBox.Controls.Add(this.outputDisableRadioButton);
            this.outputGroupBox.Location = new System.Drawing.Point(573, 38);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(235, 193);
            this.outputGroupBox.TabIndex = 1;
            this.outputGroupBox.TabStop = false;
            this.outputGroupBox.Text = "Output Buffer";
            // 
            // outputEnableRadioButton
            // 
            this.outputEnableRadioButton.AutoSize = true;
            this.outputEnableRadioButton.Location = new System.Drawing.Point(7, 127);
            this.outputEnableRadioButton.Name = "outputEnableRadioButton";
            this.outputEnableRadioButton.Size = new System.Drawing.Size(142, 36);
            this.outputEnableRadioButton.TabIndex = 1;
            this.outputEnableRadioButton.Text = "Enable";
            this.outputEnableRadioButton.UseVisualStyleBackColor = true;
            this.outputEnableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // outputDisableRadioButton
            // 
            this.outputDisableRadioButton.AutoSize = true;
            this.outputDisableRadioButton.Checked = true;
            this.outputDisableRadioButton.Location = new System.Drawing.Point(7, 64);
            this.outputDisableRadioButton.Name = "outputDisableRadioButton";
            this.outputDisableRadioButton.Size = new System.Drawing.Size(148, 36);
            this.outputDisableRadioButton.TabIndex = 0;
            this.outputDisableRadioButton.TabStop = true;
            this.outputDisableRadioButton.Text = "Disable";
            this.outputDisableRadioButton.UseVisualStyleBackColor = true;
            this.outputDisableRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // functionGroupBox
            // 
            this.functionGroupBox.Controls.Add(this.alternate3RadioButton);
            this.functionGroupBox.Controls.Add(this.alternate2RadioButton);
            this.functionGroupBox.Controls.Add(this.alternate1RadioButton);
            this.functionGroupBox.Controls.Add(this.primaryRadioButton);
            this.functionGroupBox.Controls.Add(this.gpioRadioButton);
            this.functionGroupBox.Location = new System.Drawing.Point(282, 38);
            this.functionGroupBox.Name = "functionGroupBox";
            this.functionGroupBox.Size = new System.Drawing.Size(264, 362);
            this.functionGroupBox.TabIndex = 0;
            this.functionGroupBox.TabStop = false;
            this.functionGroupBox.Text = "Function";
            // 
            // alternate3RadioButton
            // 
            this.alternate3RadioButton.AutoSize = true;
            this.alternate3RadioButton.Location = new System.Drawing.Point(21, 293);
            this.alternate3RadioButton.Name = "alternate3RadioButton";
            this.alternate3RadioButton.Size = new System.Drawing.Size(190, 36);
            this.alternate3RadioButton.TabIndex = 4;
            this.alternate3RadioButton.Text = "Alternate 3";
            this.alternate3RadioButton.UseVisualStyleBackColor = true;
            this.alternate3RadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // alternate2RadioButton
            // 
            this.alternate2RadioButton.AutoSize = true;
            this.alternate2RadioButton.Location = new System.Drawing.Point(21, 241);
            this.alternate2RadioButton.Name = "alternate2RadioButton";
            this.alternate2RadioButton.Size = new System.Drawing.Size(190, 36);
            this.alternate2RadioButton.TabIndex = 3;
            this.alternate2RadioButton.Text = "Alternate 2";
            this.alternate2RadioButton.UseVisualStyleBackColor = true;
            this.alternate2RadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // alternate1RadioButton
            // 
            this.alternate1RadioButton.AutoSize = true;
            this.alternate1RadioButton.Location = new System.Drawing.Point(21, 187);
            this.alternate1RadioButton.Name = "alternate1RadioButton";
            this.alternate1RadioButton.Size = new System.Drawing.Size(190, 36);
            this.alternate1RadioButton.TabIndex = 2;
            this.alternate1RadioButton.Text = "Alternate 1";
            this.alternate1RadioButton.UseVisualStyleBackColor = true;
            this.alternate1RadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // primaryRadioButton
            // 
            this.primaryRadioButton.AutoSize = true;
            this.primaryRadioButton.Location = new System.Drawing.Point(21, 127);
            this.primaryRadioButton.Name = "primaryRadioButton";
            this.primaryRadioButton.Size = new System.Drawing.Size(149, 36);
            this.primaryRadioButton.TabIndex = 1;
            this.primaryRadioButton.Text = "Primary";
            this.primaryRadioButton.UseVisualStyleBackColor = true;
            this.primaryRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // gpioRadioButton
            // 
            this.gpioRadioButton.AutoSize = true;
            this.gpioRadioButton.Checked = true;
            this.gpioRadioButton.Location = new System.Drawing.Point(21, 64);
            this.gpioRadioButton.Name = "gpioRadioButton";
            this.gpioRadioButton.Size = new System.Drawing.Size(122, 36);
            this.gpioRadioButton.TabIndex = 0;
            this.gpioRadioButton.TabStop = true;
            this.gpioRadioButton.Text = "GPIO";
            this.gpioRadioButton.UseVisualStyleBackColor = true;
            this.gpioRadioButton.CheckedChanged += new System.EventHandler(this.pinCfgRadioButton_CheckedChanged);
            // 
            // udsRoutineLabel
            // 
            this.udsRoutineLabel.AutoSize = true;
            this.udsRoutineLabel.Location = new System.Drawing.Point(42, 170);
            this.udsRoutineLabel.Name = "udsRoutineLabel";
            this.udsRoutineLabel.Size = new System.Drawing.Size(180, 32);
            this.udsRoutineLabel.TabIndex = 11;
            this.udsRoutineLabel.Text = "UDS Routine";
            // 
            // udsRoutineNumericUpDown
            // 
            this.udsRoutineNumericUpDown.Hexadecimal = true;
            this.udsRoutineNumericUpDown.Location = new System.Drawing.Point(42, 208);
            this.udsRoutineNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udsRoutineNumericUpDown.Name = "udsRoutineNumericUpDown";
            this.udsRoutineNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.udsRoutineNumericUpDown.TabIndex = 12;
            this.udsRoutineNumericUpDown.ValueChanged += new System.EventHandler(this.udsRoutineNumericUpDown_ValueChanged);
            // 
            // pinNumberLabel
            // 
            this.pinNumberLabel.AutoSize = true;
            this.pinNumberLabel.Location = new System.Drawing.Point(36, 62);
            this.pinNumberLabel.Name = "pinNumberLabel";
            this.pinNumberLabel.Size = new System.Drawing.Size(160, 32);
            this.pinNumberLabel.TabIndex = 9;
            this.pinNumberLabel.Text = "Pin number";
            // 
            // pinNumberComboBox
            // 
            this.pinNumberComboBox.FormattingEnabled = true;
            this.pinNumberComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255",
            "256",
            "257",
            "258",
            "259",
            "260",
            "261",
            "262",
            "263",
            "264",
            "265",
            "266",
            "267",
            "268",
            "269",
            "270",
            "271",
            "272",
            "273",
            "274",
            "275",
            "276",
            "277",
            "278",
            "279",
            "280",
            "281",
            "282",
            "283",
            "284",
            "285",
            "286",
            "287",
            "288",
            "289",
            "290",
            "291",
            "292",
            "293",
            "294",
            "295",
            "296",
            "297",
            "298",
            "299",
            "300",
            "301",
            "302",
            "303",
            "304",
            "305",
            "306",
            "307",
            "308",
            "309",
            "310",
            "311",
            "312",
            "313",
            "314",
            "315",
            "316",
            "317",
            "318",
            "319",
            "320",
            "321",
            "322",
            "323",
            "324",
            "325",
            "326",
            "327",
            "328",
            "329",
            "330",
            "331",
            "332",
            "333",
            "334",
            "335",
            "336",
            "337",
            "338",
            "339",
            "340",
            "341",
            "342",
            "343",
            "344",
            "345",
            "346",
            "347",
            "348",
            "349",
            "350",
            "351",
            "352",
            "353",
            "354",
            "355",
            "356",
            "357",
            "358",
            "359",
            "360",
            "361",
            "362",
            "363",
            "364",
            "365",
            "366",
            "367",
            "368",
            "369",
            "370",
            "371",
            "372",
            "373",
            "374",
            "375",
            "376",
            "377",
            "378",
            "379",
            "380",
            "381",
            "382",
            "383",
            "384",
            "385",
            "386",
            "387",
            "388",
            "389",
            "390",
            "391",
            "392",
            "393",
            "394",
            "395",
            "396",
            "397",
            "398",
            "399",
            "400",
            "401",
            "402",
            "403",
            "404",
            "405",
            "406",
            "407",
            "408",
            "409",
            "410",
            "411",
            "412",
            "413",
            "414",
            "415",
            "416",
            "417",
            "418",
            "419",
            "420",
            "421",
            "422",
            "423",
            "424",
            "425",
            "426",
            "427",
            "428",
            "429",
            "430",
            "431",
            "432",
            "433",
            "434",
            "435",
            "436",
            "437",
            "438",
            "439",
            "440",
            "441",
            "442",
            "443",
            "444",
            "445",
            "446",
            "447",
            "448",
            "449",
            "450",
            "451",
            "452",
            "453",
            "454",
            "455",
            "456",
            "457",
            "458",
            "459",
            "460",
            "461",
            "462",
            "463",
            "464",
            "465",
            "466",
            "467",
            "468",
            "469",
            "470",
            "471",
            "472",
            "473",
            "474",
            "475",
            "476",
            "477",
            "478",
            "479",
            "480",
            "481",
            "482",
            "483",
            "484",
            "485",
            "486",
            "487",
            "488",
            "489",
            "490",
            "491",
            "492",
            "493",
            "494",
            "495",
            "496",
            "497",
            "498",
            "499",
            "500",
            "501",
            "502",
            "503",
            "504",
            "505",
            "506",
            "507",
            "508",
            "509",
            "510",
            "511",
            "512"});
            this.pinNumberComboBox.Location = new System.Drawing.Point(36, 100);
            this.pinNumberComboBox.Name = "pinNumberComboBox";
            this.pinNumberComboBox.Size = new System.Drawing.Size(121, 39);
            this.pinNumberComboBox.TabIndex = 10;
            this.pinNumberComboBox.Text = "0";
            this.pinNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.pinNumberComboBox_SelectedIndexChanged);
            // 
            // transmissionControlGroupBox
            // 
            this.transmissionControlGroupBox.Controls.Add(this.startTransmisionButton);
            this.transmissionControlGroupBox.Location = new System.Drawing.Point(630, 559);
            this.transmissionControlGroupBox.Name = "transmissionControlGroupBox";
            this.transmissionControlGroupBox.Size = new System.Drawing.Size(1165, 285);
            this.transmissionControlGroupBox.TabIndex = 2;
            this.transmissionControlGroupBox.TabStop = false;
            this.transmissionControlGroupBox.Text = "Transmission Control";
            // 
            // startTransmisionButton
            // 
            this.startTransmisionButton.Location = new System.Drawing.Point(48, 95);
            this.startTransmisionButton.Name = "startTransmisionButton";
            this.startTransmisionButton.Size = new System.Drawing.Size(201, 45);
            this.startTransmisionButton.TabIndex = 0;
            this.startTransmisionButton.Text = "Start";
            this.startTransmisionButton.UseVisualStyleBackColor = true;
            this.startTransmisionButton.Click += new System.EventHandler(this.startTransmisionButton_Click);
            // 
            // KoenigseggHWTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2687, 1091);
            this.Controls.Add(this.transmissionControlGroupBox);
            this.Controls.Add(this.frameDataGroupBox);
            this.Controls.Add(this.framePropertiesGroupBox);
            this.Name = "KoenigseggHWTest";
            this.Text = "KoenigseggHWTest";
            this.framePropertiesGroupBox.ResumeLayout(false);
            this.framePropertiesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameIDNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.framePeriodNumericUpDown)).EndInit();
            this.frameDataGroupBox.ResumeLayout(false);
            this.frameDataGroupBox.PerformLayout();
            this.pullSelectGroupBox.ResumeLayout(false);
            this.pullSelectGroupBox.PerformLayout();
            this.pullControlGroupBox.ResumeLayout(false);
            this.pullControlGroupBox.PerformLayout();
            this.slewRateControlGroupBox.ResumeLayout(false);
            this.slewRateControlGroupBox.PerformLayout();
            this.inputHysteresisGroupBox.ResumeLayout(false);
            this.inputHysteresisGroupBox.PerformLayout();
            this.openDrainGroupBox.ResumeLayout(false);
            this.openDrainGroupBox.PerformLayout();
            this.driveStrGroupBox.ResumeLayout(false);
            this.driveStrGroupBox.PerformLayout();
            this.inputGroupBox.ResumeLayout(false);
            this.inputGroupBox.PerformLayout();
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.functionGroupBox.ResumeLayout(false);
            this.functionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udsRoutineNumericUpDown)).EndInit();
            this.transmissionControlGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox framePropertiesGroupBox;
        private System.Windows.Forms.GroupBox frameDataGroupBox;
        private System.Windows.Forms.GroupBox transmissionControlGroupBox;
        private System.Windows.Forms.Label frameIDLabel;
        private System.Windows.Forms.NumericUpDown framePeriodNumericUpDown;
        private System.Windows.Forms.Label framePeriodLabel;
        private System.Windows.Forms.NumericUpDown frameIDNumericUpDown;
        private System.Windows.Forms.Label pinNumberLabel;
        private System.Windows.Forms.ComboBox pinNumberComboBox;
        private System.Windows.Forms.CheckBox frameIdCheckBox;
        private System.Windows.Forms.TextBox frameIdTextBox;
        private System.Windows.Forms.Label udsRoutineLabel;
        private System.Windows.Forms.NumericUpDown udsRoutineNumericUpDown;
        private System.Windows.Forms.GroupBox functionGroupBox;
        private System.Windows.Forms.RadioButton alternate3RadioButton;
        private System.Windows.Forms.RadioButton alternate2RadioButton;
        private System.Windows.Forms.RadioButton alternate1RadioButton;
        private System.Windows.Forms.RadioButton primaryRadioButton;
        private System.Windows.Forms.RadioButton gpioRadioButton;
        private System.Windows.Forms.GroupBox driveStrGroupBox;
        private System.Windows.Forms.RadioButton maximumRadioButton;
        private System.Windows.Forms.RadioButton mediumHighRadioButton;
        private System.Windows.Forms.RadioButton mediumRadioButton;
        private System.Windows.Forms.RadioButton minimumRadioButton;
        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.RadioButton inputEnableRadioButton;
        private System.Windows.Forms.RadioButton inputDisableRadioButton;
        private System.Windows.Forms.GroupBox outputGroupBox;
        private System.Windows.Forms.RadioButton outputEnableRadioButton;
        private System.Windows.Forms.RadioButton outputDisableRadioButton;
        private System.Windows.Forms.GroupBox slewRateControlGroupBox;
        private System.Windows.Forms.RadioButton fullWoSlewCtrlRadioButton;
        private System.Windows.Forms.RadioButton halfWoSlewCtrlRadioButton;
        private System.Windows.Forms.RadioButton fullWSlewCtrlRadioButton;
        private System.Windows.Forms.RadioButton halfWSlewCtrlRadioButton;
        private System.Windows.Forms.GroupBox inputHysteresisGroupBox;
        private System.Windows.Forms.RadioButton inputHysteresisEnableRadioButton;
        private System.Windows.Forms.RadioButton inputHysteresisDisableRadioButton;
        private System.Windows.Forms.GroupBox openDrainGroupBox;
        private System.Windows.Forms.RadioButton openDrainEnableRadioButton;
        private System.Windows.Forms.RadioButton openDrainDisableRadioButton;
        private System.Windows.Forms.GroupBox pullSelectGroupBox;
        private System.Windows.Forms.RadioButton pullUpRadioButton;
        private System.Windows.Forms.RadioButton pulldownRadioButton;
        private System.Windows.Forms.GroupBox pullControlGroupBox;
        private System.Windows.Forms.RadioButton pullEnableRadioButton;
        private System.Windows.Forms.RadioButton pullDisableRadioButton;
        private System.Windows.Forms.Button startTransmisionButton;
    }
}

