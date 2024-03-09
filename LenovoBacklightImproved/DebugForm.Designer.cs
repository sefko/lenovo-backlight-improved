namespace LenovoBacklightImproved
{
    partial class DebugForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            debugTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // debugTextBox
            // 
            debugTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            debugTextBox.Location = new Point(12, 12);
            debugTextBox.Name = "debugTextBox";
            debugTextBox.ReadOnly = true;
            debugTextBox.Size = new Size(434, 249);
            debugTextBox.TabIndex = 6;
            debugTextBox.Text = "";
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(458, 273);
            Controls.Add(debugTextBox);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DebugForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Backlight Debug";
            FormClosing += DebugForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox debugTextBox;
    }
}
