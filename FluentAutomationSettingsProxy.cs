using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation;

namespace ScriptCs.FluentAutomation
{
    // Seperate settings block that only applies to ScriptCs execution
    internal static class ScriptCsSettings
    {
        private static bool prettyPrintResults = true;
        internal static bool PrettyPrintResults
        {
            get { return prettyPrintResults; }
            set { prettyPrintResults = value; }
        }
    }

    public class FluentAutomationSettingsProxy
    {
        public bool PrettyPrintResults
        {
            get { return ScriptCsSettings.PrettyPrintResults; }
            set { ScriptCsSettings.PrettyPrintResults = value; }
        }

        public string UserTempDirectory
        {
            get
            {
                return Settings.UserTempDirectory;
            }

            set
            {
                Settings.UserTempDirectory = value;
            }
        }

        public string ScreenshotPath
        {
            get { return Settings.ScreenshotPath; }
            set { Settings.ScreenshotPath = value; }
        }

        public bool ScreenshotOnFailedExpect
        {
            get { return Settings.ScreenshotOnFailedExpect; }
            set { Settings.ScreenshotOnFailedExpect = value; }
        }

        public bool ScreenshotOnFailedAction
        {
            get { return Settings.ScreenshotOnFailedAction; }
            set { Settings.ScreenshotOnFailedAction = value; }
        }

        public TimeSpan DefaultWaitTimeout
        {
            get { return Settings.DefaultWaitTimeout; }
            set { Settings.DefaultWaitTimeout = value; }
        }

        /// <summary>
        /// Time to wait before assuming the provided WaitUntil() condition will never be reached. Defaults to 30 seconds.
        /// </summary>
        public TimeSpan DefaultWaitUntilTimeout
        {
            get { return Settings.DefaultWaitUntilTimeout; }
            set { Settings.DefaultWaitUntilTimeout = value; }
        }

        /// <summary>
        /// Time to wait before attempting to validate the provided condition for WatiUntil(). Defaults to 100 milliseconds.
        /// </summary>
        public TimeSpan DefaultWaitUntilThreadSleep
        {
            get { return Settings.DefaultWaitUntilThreadSleep; }
            set { Settings.DefaultWaitUntilThreadSleep = value; }
        }

        /// <summary>
        /// Wait on all comamnds to be successful. Removes the need for explicit I.WaitUntil() calls for actions
        /// </summary>
        public bool WaitOnAllCommands
        {
            get { return Settings.WaitOnAllCommands; }
            set { Settings.WaitOnAllCommands = value; }
        }

        /// <summary>
        /// Wait on all I.Expect.* actions to be successful. Removes the need for explicit I.WaitUntil() calls.
        /// </summary>
        public bool WaitOnAllExpects
        {
            get { return Settings.WaitOnAllExpects; }
            set { Settings.WaitOnAllExpects = value; }
        }

        /// <summary>
        /// Determines whether or not windows will automatically be minimized on start of test execution and reverted when finished. Defaults to true.
        /// </summary>
        public bool MinimizeAllWindowsOnTestStart
        {
            get { return Settings.MinimizeAllWindowsOnTestStart; }
            set { Settings.MinimizeAllWindowsOnTestStart = value; }
        }

        /// <summary>
        /// Determines the height of the automated browser window. Defaults to null, which will use the provider defaults.
        /// </summary>
        public int? WindowHeight
        {
            get { return Settings.WindowHeight; }
            set { Settings.WindowHeight = value; }
        }

        /// <summary>
        /// Determines the width of the automated browser window. Defaults to null, which will use the provider defaults.
        /// </summary>
        public int? WindowWidth
        {
            get { return Settings.WindowWidth; }
            set { Settings.WindowWidth = value; }
        }
    }
}
