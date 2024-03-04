using System.Diagnostics;

namespace LenovoBacklightImproved
{
    internal class SystemTray : ApplicationContext
    {
        private Program mainProgram;

        private NotifyIcon trayIcon;

        private Thread? configThread;
        private ConfigureForm? configForm;

        public SystemTray(Program mainProgram)
        {
            this.mainProgram = mainProgram;

            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.Final,
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = { 
                        new ToolStripMenuItem("Configure", null, Configure),
                        new ToolStripMenuItem("Exit", null, Exit),
                    }
                },
                Visible = true
            };

            trayIcon.MouseDoubleClick += Configure;
        }

        private void Exit(object? sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Configure(object? sender, EventArgs e)
        {
            if (configThread == null || configThread.IsAlive == false)
            {
                configThread = new Thread(() =>
                {
                    configForm = new ConfigureForm(mainProgram);
                    Application.Run(configForm);
                });
                configThread.SetApartmentState(ApartmentState.STA);
                configThread.Start();
            }
            else if (configThread != null && configThread.IsAlive == true)
            {
                if (configForm != null)
                {
                    configForm.bringFormToFront();
                }
            } 
        }
    }
}
