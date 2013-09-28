using FluentAutomation;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;
using ScriptCs.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.FluentAutomation
{
    public class F14N : IScriptPackContext
    {
        public F14N()
        {
            if (!hasShownWarning)
            {
                hasShownWarning = true;
                ConsoleHelper.Clear();
                Console.WriteLine();
                ConsoleHelper.DrawMessageBox(2, string.Format("F14N - Fluent Automation Console{0}Testing will begin momentarily. You may see output from the browsers in this console, ignore it.", Environment.NewLine), true);
                Console.WriteLine();
            }
        }

        internal IEnumerable<MethodInfo> bootstrapMethods = null;

        internal static bool hasShownWarning = false;
                
        public static DateTime? StartTime = null;

        public static List<TestResult> Results = new List<TestResult>();

        public F14N Init<T>()
        {
            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Static | BindingFlags.Public);
            this.bootstrapMethods = methods.Where(x => x.Name == "Bootstrap");

            return this;
        }

        public F14N Bootstrap()
        {
            var method = this.bootstrapMethods.FirstOrDefault(m => m.GetParameters().Length == 0);
            if (method == null)
                throw new FluentException("No matching Bootstrap method located for the selected provider.");

            method.Invoke(null, new object[] { });

            return this;
        }

        public F14N Bootstrap(string browser)
        {
            ParameterInfo[] parameters = null;
            var method = this.bootstrapMethods.FirstOrDefault(m =>
            {
                parameters = m.GetParameters();
                if (parameters.Length == 1 && parameters.First().ParameterType.BaseType == typeof(Enum))
                    return true;

                return false;
            });

            if (method == null)
                throw new FluentException("No matching Bootstrap method located for the selected provider.");

            var browserEnumValue = Enum.Parse(parameters[0].ParameterType, browser);
            method.Invoke(null, new object[] { browserEnumValue });

            return this;
        }

        public F14N Bootstrap(Browser browser)
        {
            return this.Bootstrap(browser.ToString());
        }

        public F14N Bootstrap(Uri remoteDriverUri, string browser)
        {
            ParameterInfo[] parameters = null;
            var method = this.bootstrapMethods.FirstOrDefault(m =>
            {
                parameters = m.GetParameters();
                if (parameters.Length == 2 && parameters[1].ParameterType.BaseType == typeof(Enum))
                    return true;

                return false;
            });

            if (method == null)
                throw new FluentException("No matching Bootstrap method located for the selected provider.");

            var browserEnumValue = Enum.Parse(parameters[1].ParameterType, browser);
            method.Invoke(null, new object[] { remoteDriverUri, browserEnumValue });

            return this;
        }

        public F14N Boostrap(Uri remoteDriverUri, Browser browser)
        {
            return this.Bootstrap(remoteDriverUri, browser.ToString());
        }

        public F14N Bootstrap(Uri remoteDriverUri, Dictionary<string, object> capabilities)
        {
            ParameterInfo[] parameters = null;
            var method = this.bootstrapMethods.FirstOrDefault(m =>
            {
                parameters = m.GetParameters();
                if (parameters.Length == 2 && parameters[1].ParameterType == typeof(Dictionary<string, object>))
                    return true;

                return false;
            });

            if (method == null)
                throw new FluentException("No matching Bootstrap method located for the selected provider.");

            method.Invoke(null, new object[] { remoteDriverUri, capabilities });

            return this;
        }

        public F14N Bootstrap(Uri remoteDriverUri, Browser browser, Dictionary<string, object> additionalCapabilities)
        {
            ParameterInfo[] parameters = null;
            var method = this.bootstrapMethods.FirstOrDefault(m =>
            {
                parameters = m.GetParameters();
                if (parameters.Length == 3 && parameters[2].ParameterType == typeof(Dictionary<string, object>))
                    return true;

                return false;
            });

            if (method == null)
                throw new FluentException("No matching Bootstrap method located for the selected provider.");

            method.Invoke(null, new object[] { remoteDriverUri, browser, additionalCapabilities });

            return this;
        }

        public F14N Config(Action<FluentAutomationSettingsProxy> action)
        {
            try
            {
                action(new FluentAutomationSettingsProxy());
            }
            catch (Exception) { };

            return this;
        }

        public F14N Run(Action action)
        {
            return Run(string.Format("Test {0}", Results.Count + 1), action);
        }

        public F14N Run(string name, Action action)
        {
            if (!StartTime.HasValue) StartTime = DateTime.Now;
            if (Results.Count != 0) Console.WriteLine();
            ConsoleHelper.DrawMessageBox(2, string.Format("Running test: {0}", name));
            Console.WriteLine();

            try
            {
                action();
                Results.Add(new TestResult()
                {
                    Name = name,
                    Passed = true
                });
            }
            catch (Exception ex)
            {
                Results.Add(new TestResult()
                {
                    Name = name,
                    Passed = false,
                    Exception = ex,
                    Message = GetExceptionMessageString(ex)
                });
            }

            return this;
        }

        public F14N Run(Action<INativeActionSyntaxProvider> action)
        {
            return Run(string.Format("Test {0}", Results.Count + 1), action);
        }

        public F14N Run(string name, Action<INativeActionSyntaxProvider> action)
        {
            if (!StartTime.HasValue) StartTime = DateTime.Now;
            if (Results.Count != 0) Console.WriteLine();
            ConsoleHelper.DrawMessageBox(2, string.Format("Running test: {0}", name));
            Console.WriteLine();

            var test = new FluentTest();

            try
            {
                var provider = test.I; // Must be accessed to initialize values
                action(provider);

                Results.Add(new TestResult()
                {
                    Name = name,
                    Passed = true
                });
            }
            catch (Exception ex)
            {
                Results.Add(new TestResult()
                {
                    Name = name,
                    Passed = false,
                    Exception = ex,
                    Message = GetExceptionMessageString(ex)
                });
            }
            finally
            {
                test.Dispose();
            }

            return this;
        }

        public F14NProxy Proxy()
        {
            return new F14NProxy(this);
        }

        internal void Terminate()
        {
            if (Results.Count == 0) return;

            if (ScriptCsSettings.PrettyPrintResults)
            {
                ConsoleHelper.Clear();
                Console.WriteLine();
                ConsoleHelper.DrawMessageBox(2, "F14N - Fluent Automation Console", true);
                Console.WriteLine();

                var index = 1;
                foreach (var result in Results)
                {
                    if (!result.Passed)
                    {
                        Console.ForegroundColor = result.Passed ? ConsoleColor.DarkGreen : ConsoleColor.Red;
                        Console.WriteLine("   {0}) {1}", index, result.Name);
                        Console.ResetColor();
                        if (result.Message != null)
                        {
                            Console.WriteLine();
                            ConsoleHelper.DrawMessageBox(6, result.Message + Environment.NewLine + result.Exception.StackTrace);
                            Console.WriteLine();
                        }
                    }

                    index++;
                }

                var timeDiff = DateTime.Now - StartTime.Value;
                string timeString = string.Empty;
                if (timeDiff.TotalSeconds <= 59)
                    timeString = string.Format("{0} seconds", timeDiff.TotalSeconds);
                else
                    if (timeDiff.Seconds == 0)
                        timeString = string.Format("{0} minutes", timeDiff.TotalMinutes);
                    else
                        timeString = string.Format("{0} minutes, {1} seconds", timeDiff.TotalMinutes, timeDiff.Seconds);

                var hasErrors = Results.Count(x => x.Passed == false) == 0;
                Console.ForegroundColor = hasErrors ? ConsoleColor.DarkGreen : ConsoleColor.Red;
                var stream = Console.Out;
                if (hasErrors)
                    stream = Console.Error;
                
                stream.WriteLine("   {0} tests complete ({1}){2}", Results.Count, timeString, Environment.NewLine);
                Console.ResetColor();
            }
            else
            {
                List<dynamic> errorResults = new List<dynamic>();
                foreach (var result in Results)
                {
                    if (!result.Passed)
                    {
                        var stackTrace = new StackTrace(result.Exception, true);
                        var errorFrame = stackTrace.GetFrame(0);

                        errorResults.Add(new
                        {
                            Column = errorFrame.GetFileColumnNumber(),
                            Row = errorFrame.GetFileLineNumber(),
                            Message = result.Message,
                            StackTrace = result.Exception.StackTrace
                        });
                    }
                }

                var resultsContainer = new
                {
                    TestCount = Results.Count,
                    ElapsedSeconds = (DateTime.Now - StartTime.Value).TotalSeconds,
                    Errors = errorResults
                };

                var stream = Console.Out;
                if (errorResults.Count > 0)
                    stream = Console.Error;

                stream.WriteLine(SimpleJson.SimpleJson.SerializeObject(resultsContainer));
            }
        }

        private string GetExceptionMessageString(Exception ex)
        {
            var errorMessage = new StringBuilder();
            if (ex.InnerException != null)
            {
                errorMessage.AppendLine(ex.InnerException.Message);
                errorMessage.AppendLine("--------");
            }

            errorMessage.AppendLine(ex.Message);
            return errorMessage.ToString();
        }
    }
}
