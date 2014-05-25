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
                return FluentSettings.Current.UserTempDirectory;
            }

            set
            {
                FluentSettings.Current.UserTempDirectory = value;
            }
        }

        public string ScreenshotPath
        {
            get { return FluentSettings.Current.ScreenshotPath; }
            set { FluentSettings.Current.ScreenshotPath = value; }
        }

        public bool ScreenshotOnFailedExpect
        {
            get { return FluentSettings.Current.ScreenshotOnFailedExpect; }
            set { FluentSettings.Current.ScreenshotOnFailedExpect = value; }
        }

        public bool ScreenshotOnFailedAction
        {
            get { return FluentSettings.Current.ScreenshotOnFailedAction; }
            set { FluentSettings.Current.ScreenshotOnFailedAction = value; }
        }

        public TimeSpan DefaultWaitTimeout
        {
            get { return FluentSettings.Current.WaitTimeout; }
            set { FluentSettings.Current.WaitTimeout = value; }
        }

        /// <summary>
        /// Time to wait before assuming the provided WaitUntil() condition will never be reached. Defaults to 30 seconds.
        /// </summary>
        public TimeSpan DefaultWaitUntilTimeout
        {
            get { return FluentSettings.Current.WaitUntilTimeout; }
            set { FluentSettings.Current.WaitUntilTimeout = value; }
        }

        /// <summary>
        /// Time to wait before attempting to validate the provided condition for WatiUntil(). Defaults to 100 milliseconds.
        /// </summary>
        public TimeSpan WaitUntilInterval
        {
            get { return FluentSettings.Current.WaitUntilInterval; }
            set { FluentSettings.Current.WaitUntilInterval = value; }
        }

        /// <summary>
        /// Wait on all comamnds to be successful. Removes the need for explicit I.WaitUntil() calls for actions
        /// </summary>
        public bool WaitOnAllActions
        {
            get { return FluentSettings.Current.WaitOnAllActions; }
            set { FluentSettings.Current.WaitOnAllActions = value; }
        }

        /// <summary>
        /// Wait on all I.Expect.* actions to be successful. Removes the need for explicit I.WaitUntil() calls.
        /// </summary>
        public bool WaitOnAllExpects
        {
            get { return FluentSettings.Current.WaitOnAllExpects; }
            set { FluentSettings.Current.WaitOnAllExpects = value; }
        }

        /// <summary>
        /// Determines whether or not windows will automatically be minimized on start of test execution and reverted when finished. Defaults to true.
        /// </summary>
        public bool MinimizeAllWindowsOnTestStart
        {
            get { return FluentSettings.Current.MinimizeAllWindowsOnTestStart; }
            set { FluentSettings.Current.MinimizeAllWindowsOnTestStart = value; }
        }

        /// <summary>
        /// Determines the height of the automated browser window. Defaults to null, which will use the provider defaults.
        /// </summary>
        public int? WindowHeight
        {
            get { return FluentSettings.Current.WindowHeight; }
            set { FluentSettings.Current.WindowHeight = value; }
        }

        /// <summary>
        /// Determines the width of the automated browser window. Defaults to null, which will use the provider defaults.
        /// </summary>
        public int? WindowWidth
        {
            get { return FluentSettings.Current.WindowWidth; }
            set { FluentSettings.Current.WindowWidth = value; }
        }
    }
}
