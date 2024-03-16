using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LenovoBacklightImproved
{
    class Program : Form
    {
        /*
         *  Internal Constants
         */
        internal const string appNameSpaced = "Lenovo Backlight Improved";
        internal static readonly string appName = appNameSpaced.Replace(" ", "");

        /*
         *  Private Constants
         */
        private const string defaultDllLocation = "C:\\ProgramData\\Lenovo";
        private readonly string[] defaultDlls = ["C:\\ProgramData\\Lenovo\\Vantage\\Addins\\ThinkKeyboardAddin\\1.0.0.18\\Keyboard_Core.dll",
                                                 "C:\\ProgramData\\Lenovo\\ImController\\Plugins\\ThinkKeyboardPlugin\\x86\\Keyboard_Core.dll"];

        /*
         *  Private Variabled
         */
        private string lastDllTried = "";

        private SystemTray? systemTray;

        private KeyboardBacklightStatusControl? keyboardBacklightStatusControl;

        private System.Timers.Timer timeoutTimer = new System.Timers.Timer();
        private uint inactivityTimeout;

        private byte selectedBacklightLevel;
        private byte currentBacklightStatus;

        private bool isStatusChangeFromUs = false;

        /*
         *  Power Setting Variables
         */
        private static Guid GUID_SYSTEM_POWER_SETTING = new Guid("45BC44C4-4E13-4F23-9C0E-9F7593BEFCA0");

        private readonly Message aboutToSleep = new Message
        {
            Msg = 0x320,
            WParam = unchecked((nint)0xe3005ba3),
        };

        private readonly Message resumeFromSleep = new Message
        {
            Msg = 0x218,
            WParam = 0x07,
        };

        /*
         *  Constructors
         */
        private Program()
        {
            try
            {
                loadDll(Properties.Settings.Default.DllPath);
            }
            catch (Exception ex)
            {
                string? defaultDll = defaultDlls.FirstOrDefault(dllPath => File.Exists(dllPath));

                if (defaultDll == null)
                {
                    defaultDll = FindFile(defaultDllLocation, "Keyboard_Core.dll");
                }

                if (defaultDll != null && Properties.Settings.Default.DllPath != defaultDll)
                {
                    try
                    {
                        loadDll(defaultDll);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show($"Configured DLL file could not be loaded: '{ex.Message}'\n" +
                                        $"Default DLL file also could not be loaded: '{ex2.Message}'", $"{appNameSpaced} - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message, $"{appNameSpaced} - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Register the power setting notification
            RegisterPowerSettingNotification(this.Handle, ref GUID_SYSTEM_POWER_SETTING, 0);

            systemTray = new SystemTray(this);

            inactivityTimeout = Properties.Settings.Default.InactivityTimeout;
            timeoutTimer.Interval = Properties.Settings.Default.CheckInterval;

            selectBacklightLevel(Properties.Settings.Default.BacklightLevel);

            Application.Run(systemTray);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, appName, out createdNew))
            {
                if (!createdNew)
                {
                    MessageBox.Show("Application is already running! Check the system tray!", appNameSpaced, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ApplicationConfiguration.Initialize();
                Application.Run(new Program());
            }
        }

        /*
         *  Miscellaneous Functions
         */
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, int Flags);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == aboutToSleep.Msg && m.WParam == aboutToSleep.WParam)
            {
                Trace.WriteLine("The system is going to sleep! Stopping timeoutTimer!");
                stopTimer();
            }
            else if (m.Msg == resumeFromSleep.Msg && m.WParam == resumeFromSleep.WParam)
            {
                Trace.WriteLine("The system has waken up! Starting timeoutTimer!");
                setBacklightStatusSafe(keyboardBacklightStatusControl, getStatusFromLevel(selectedBacklightLevel));
                startTimer();
            }

            // Call the base method
            base.WndProc(ref m);
        }
        
        /*
         *  Internal Helper Functions
         */
        internal void selectBacklightLevel(byte newBacklightLevel)
        {
            selectedBacklightLevel = newBacklightLevel;
            Properties.Settings.Default.BacklightLevel = selectedBacklightLevel;
            Properties.Settings.Default.Save();
            
            if (systemTray != null)
            {
                systemTray.updateLevelsChecked();
            }
        }

        internal byte getSelectedBacklightLevel()
        {
            return selectedBacklightLevel;
        }

        /*
         *  Private Helper Functions
         */
        private byte getStatusFromLevel(byte level)
        {
            return level > 2 ? (byte)(level % 3 + 1) : level;
        }

        private void startTimer()
        {
            SystemIdle.makeNotIdle();
            timeoutTimer.Start();
        }

        private void stopTimer()
        {
            timeoutTimer.Stop();
        }

        private void setBacklightStatusSafe(KeyboardBacklightStatusControl? keyboardBacklightStatusControl, byte expectedStatus)
        {
            if (keyboardBacklightStatusControl != null)
            {
                keyboardBacklightStatusControl.SetStatus(expectedStatus);
                currentBacklightStatus = expectedStatus;
                isStatusChangeFromUs = true;
            }
        }

        private void checkAndCorrectBacklightStatus(object? sender, System.Timers.ElapsedEventArgs e, KeyboardBacklightStatusControl keyboardBacklightStatusControl)
        {
            uint idleTime = SystemIdle.getIdleTime();
            byte newBacklightStatus = keyboardBacklightStatusControl.GetStatus();
            byte expectedStatus = getStatusFromLevel(selectedBacklightLevel);

            Trace.WriteLine($"Idle time: {idleTime}");
            Trace.WriteLine($"Backlight status check: {currentBacklightStatus}, {newBacklightStatus}, {isStatusChangeFromUs}");

            if (newBacklightStatus != currentBacklightStatus && isStatusChangeFromUs == false)
            {
                selectBacklightLevel((byte)((selectedBacklightLevel + 1) % 5));
                SystemIdle.makeNotIdle();

                Trace.WriteLine($"Backlight level change: {selectedBacklightLevel}");

                expectedStatus = getStatusFromLevel(selectedBacklightLevel);

                if (expectedStatus != newBacklightStatus)
                {
                    setBacklightStatusSafe(keyboardBacklightStatusControl, expectedStatus);
                    Trace.WriteLine($"Change needed!");
                }
                else
                {
                    currentBacklightStatus = expectedStatus;
                    Trace.WriteLine($"No change needed!");
                }
            }
            else if (isStatusChangeFromUs)
            {
                isStatusChangeFromUs = false;
            }

            Trace.WriteLine($"Current backlight level: {selectedBacklightLevel}");

            if (selectedBacklightLevel > 2)
            {
                if (idleTime >= inactivityTimeout && newBacklightStatus != 0)
                {
                    setBacklightStatusSafe(keyboardBacklightStatusControl, 0);
                    Trace.WriteLine($"Idle! Shutting off!");
                }
                else if (idleTime < inactivityTimeout && newBacklightStatus != getStatusFromLevel(selectedBacklightLevel))
                {
                    setBacklightStatusSafe(keyboardBacklightStatusControl, expectedStatus);
                    Trace.WriteLine($"Not idle! Setting to {expectedStatus}");
                }
            }
            else if (getStatusFromLevel(selectedBacklightLevel) != newBacklightStatus)
            {
                setBacklightStatusSafe(keyboardBacklightStatusControl, getStatusFromLevel(selectedBacklightLevel));
            }
        }
        private static string? FindFile(string path, string fileSearched)
        {
            try
            {
                string? foundFile = Directory.GetFiles(path).FirstOrDefault(file => file.EndsWith(fileSearched));

                if (foundFile != null)
                {
                    return foundFile;
                }

                string[] dirs = Directory.GetDirectories(path);

                foreach (string dir in dirs)
                {
                    foundFile = FindFile(dir, fileSearched);
                    
                    if (foundFile != null)
                    {
                        return foundFile;
                    }
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                // No access to folder! Move on!
            }

            return null;
        }

        /*
         *  Public Getter Functions
         */
        public uint getInactivityTimeout()
        {
            return inactivityTimeout;
        }

        public uint getTimerInterval()
        {
            return (uint)timeoutTimer.Interval;
        }

        public string getDllPath()
        {
            if (keyboardBacklightStatusControl != null)
            {
                return keyboardBacklightStatusControl.getDll();
            }

            return "No DLL loaded!";
        }

        public string getLastDllTried()
        {
            return lastDllTried;
        }

        /*
         *  Public Setter Functions
         */
        public void setInactivityTimeout(uint newTimeout)
        {
            inactivityTimeout = newTimeout;
        }

        public void setTimerInterval(uint newInterval)
        {
            stopTimer();
            timeoutTimer.Interval = newInterval;
            startTimer();
        }

        public void loadDll(string dllPath)
        {
            if (timeoutTimer.Enabled == true)
            {
                stopTimer();
            }

            lastDllTried = dllPath;
            keyboardBacklightStatusControl = new KeyboardBacklightStatusControl(dllPath);

            setBacklightStatusSafe(keyboardBacklightStatusControl, getStatusFromLevel(selectedBacklightLevel));

            timeoutTimer.Elapsed += (sender, e) => checkAndCorrectBacklightStatus(sender, e, keyboardBacklightStatusControl);
            startTimer();
        }
    }
}