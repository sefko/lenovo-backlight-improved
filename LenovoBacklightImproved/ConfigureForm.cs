using System.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LenovoBacklightImproved
{
    public partial class ConfigureForm : Form
    {
        private Program mainProgram;

        internal ConfigureForm(Program mainProgram)
        {
            InitializeComponent();

            this.mainProgram = mainProgram;
            
            Visible = true;

            timeoutNum.Value = mainProgram.getInactivityTimeout() / 1000;
            intervalNum.Value = mainProgram.getTimerInterval();
            dllTextBox.Text = mainProgram.getDllPath();
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        internal void bringFormToFront()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(bringFormToFront));
            }
            else
            {
                SetForegroundWindow(Handle);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            mainProgram.setInactivityTimeout((uint)(timeoutNum.Value * 1000));
            mainProgram.setTimerInterval((uint)intervalNum.Value);

            try
            {
                mainProgram.loadDll(dllTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"{Program.appNameSpaced} - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DllPath = mainProgram.getDllPath();
            Properties.Settings.Default.InactivityTimeout = mainProgram.getInactivityTimeout();
            Properties.Settings.Default.CheckInterval = mainProgram.getTimerInterval();
            Properties.Settings.Default.Save();
        }

        private void btnLoadDll_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = mainProgram.getLastDllTried();
            fileDialog.Filter = "Dll files (*.dll)|*.dll|All files|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                dllTextBox.Text = fileDialog.FileName;
            }
        }
    }
}
