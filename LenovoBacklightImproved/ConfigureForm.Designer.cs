namespace LenovoBacklightImproved
{
    partial class ConfigureForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureForm));
            timeoutLabel = new Label();
            timeoutNum = new NumericUpDown();
            intervalLabel = new Label();
            intervalNum = new NumericUpDown();
            dllLabel = new Label();
            dllTextBox = new TextBox();
            btnLoadDll = new Button();
            btnApply = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)timeoutNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)intervalNum).BeginInit();
            SuspendLayout();
            // 
            // timeoutLabel
            // 
            timeoutLabel.AutoSize = true;
            timeoutLabel.Location = new Point(10, 7);
            timeoutLabel.Name = "timeoutLabel";
            timeoutLabel.Size = new Size(109, 15);
            timeoutLabel.TabIndex = 0;
            timeoutLabel.Text = "Timeout              (s.)";
            // 
            // timeoutNum
            // 
            timeoutNum.DecimalPlaces = 2;
            timeoutNum.Location = new Point(140, 5);
            timeoutNum.Margin = new Padding(3, 2, 3, 2);
            timeoutNum.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            timeoutNum.Minimum = new decimal(new int[] { 25, 0, 0, 65536 });
            timeoutNum.Name = "timeoutNum";
            timeoutNum.Size = new Size(117, 23);
            timeoutNum.TabIndex = 1;
            timeoutNum.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // intervalLabel
            // 
            intervalLabel.AutoSize = true;
            intervalLabel.Location = new Point(10, 32);
            intervalLabel.Name = "intervalLabel";
            intervalLabel.Size = new Size(112, 15);
            intervalLabel.TabIndex = 2;
            intervalLabel.Text = "Check interval (ms.)";
            // 
            // intervalNum
            // 
            intervalNum.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            intervalNum.Location = new Point(140, 30);
            intervalNum.Margin = new Padding(3, 2, 3, 2);
            intervalNum.Maximum = new decimal(new int[] { 2500, 0, 0, 0 });
            intervalNum.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            intervalNum.Name = "intervalNum";
            intervalNum.Size = new Size(117, 23);
            intervalNum.TabIndex = 3;
            intervalNum.Value = new decimal(new int[] { 250, 0, 0, 0 });
            // 
            // dllLabel
            // 
            dllLabel.AutoSize = true;
            dllLabel.Location = new Point(10, 57);
            dllLabel.Name = "dllLabel";
            dllLabel.Size = new Size(27, 15);
            dllLabel.TabIndex = 4;
            dllLabel.Text = "DLL";
            // 
            // dllTextBox
            // 
            dllTextBox.Location = new Point(45, 54);
            dllTextBox.Margin = new Padding(3, 2, 3, 2);
            dllTextBox.Name = "dllTextBox";
            dllTextBox.Size = new Size(212, 23);
            dllTextBox.TabIndex = 5;
            // 
            // btnLoadDll
            // 
            btnLoadDll.Location = new Point(10, 80);
            btnLoadDll.Margin = new Padding(3, 2, 3, 2);
            btnLoadDll.Name = "btnLoadDll";
            btnLoadDll.Size = new Size(79, 22);
            btnLoadDll.TabIndex = 6;
            btnLoadDll.Text = "Load DLL";
            btnLoadDll.UseVisualStyleBackColor = true;
            btnLoadDll.Click += btnLoadDll_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(94, 80);
            btnApply.Margin = new Padding(3, 2, 3, 2);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(79, 22);
            btnApply.TabIndex = 7;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(178, 80);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(79, 22);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ConfigureForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(268, 112);
            Controls.Add(btnSave);
            Controls.Add(btnApply);
            Controls.Add(btnLoadDll);
            Controls.Add(dllTextBox);
            Controls.Add(dllLabel);
            Controls.Add(intervalNum);
            Controls.Add(intervalLabel);
            Controls.Add(timeoutNum);
            Controls.Add(timeoutLabel);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ConfigureForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Backlight Config";
            ((System.ComponentModel.ISupportInitialize)timeoutNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)intervalNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label timeoutLabel;
        private NumericUpDown timeoutNum;
        private Label intervalLabel;
        private NumericUpDown intervalNum;
        private Label dllLabel;
        private TextBox dllTextBox;
        private Button btnLoadDll;
        private Button btnApply;
        private Button btnSave;
    }
}
