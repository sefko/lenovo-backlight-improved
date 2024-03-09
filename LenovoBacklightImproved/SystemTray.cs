using System.Diagnostics;
using System.Reflection.Metadata;

namespace LenovoBacklightImproved
{
    internal class SystemTray : ApplicationContext
    {
        private Program mainProgram;

        private NotifyIcon trayIcon;

        private Thread? configThread;
        private Thread? debugThread;
        private ConfigureForm? configForm;
        private DebugForm? debugForm;

        public SystemTray(Program mainProgram)
        {
            this.mainProgram = mainProgram;

            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.Final,
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = {
                        new ToolStripMenuItem("Select state", null, [
                            new ToolStripMenuItem("Off", null, StateSelector, "0"),
                            new ToolStripMenuItem("Level 1 without Timeout", null, StateSelector, "1"),
                            new ToolStripMenuItem("Level 2 without Timeout", null, StateSelector, "2"),
                            new ToolStripMenuItem("Level 1 with Timeout", null, StateSelector, "3"),
                            new ToolStripMenuItem("Level 2 with Timeout", null, StateSelector, "4"),
                        ]),
                        new ToolStripMenuItem("Configure", null, Configure),
                        new ToolStripMenuItem("Debug console", null, DebugConsole),
                        new ToolStripMenuItem("Exit", null, Exit),
                    }
                },
                Visible = true
            };

            trayIcon.MouseDoubleClick += Configure;
        }

        internal void updateLevelsChecked()
        {
            if (mainProgram.InvokeRequired)
            {
                mainProgram.BeginInvoke(new Action(updateLevelsChecked));
            }
            else
            {
                if (trayIcon.ContextMenuStrip != null)
                {
                    ToolStripMenuItem[] stateSelectorItems = new ToolStripMenuItem[5];
                    ((ToolStripMenuItem)trayIcon.ContextMenuStrip.Items[0]).DropDownItems.CopyTo(stateSelectorItems, 0);

                    ToolStripMenuItem selectedState = stateSelectorItems[mainProgram.getSelectedBacklightLevel()];

                    foreach (ToolStripMenuItem item in stateSelectorItems)
                    {
                        item.Checked = false;
                    }

                    selectedState.Checked = true;
                }
            }            
        }

        private void StateSelector(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                ToolStripMenuItem selectedLevelMenuItem = (ToolStripMenuItem)sender;
                mainProgram.selectBacklightLevel(Convert.ToByte(selectedLevelMenuItem.Name));
            }
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
        private void DebugConsole(object? sender, EventArgs e)
        {
            if (debugThread == null || debugThread.IsAlive == false)
            {
                debugThread = new Thread(() =>
                {
                    debugForm = new DebugForm(mainProgram);
                    Application.Run(debugForm);
                });
                debugThread.SetApartmentState(ApartmentState.STA);
                debugThread.Start();
            }
            else if (debugThread != null && debugThread.IsAlive == true)
            {
                if (debugForm != null)
                {
                    debugForm.bringFormToFront();
                }
            }
        }

        private void Exit(object? sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
