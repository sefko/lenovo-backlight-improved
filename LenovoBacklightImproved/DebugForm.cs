using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LenovoBacklightImproved
{
    public partial class DebugForm : Form
    {
        private Program mainProgram;
        private MyTraceListener traceListener;

        internal DebugForm(Program mainProgram)
        {
            InitializeComponent();

            this.mainProgram = mainProgram;
            this.traceListener = new MyTraceListener(debugTextBox);

            Visible = true;

            Trace.Listeners.Add(this.traceListener);
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

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace.Listeners.Remove(this.traceListener);
        }
    }

    public class MyTraceListener : TraceListener
    {
        private TextBoxBase output;

        public MyTraceListener(TextBoxBase output)
        {
            this.Name = "Trace";
            this.output = output;
        }

        public override void Write(string message)
        {
            Action append = delegate ()
            {
                output.AppendText($"[{DateTime.Now.ToString()}] ");
                output.AppendText(message);
            };

            if (output.InvokeRequired)
            {
                output.BeginInvoke(append);
            }
            else
            {
                append();
            }
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}
