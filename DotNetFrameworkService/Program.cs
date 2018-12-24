using System;
using Topshelf;
using System.Windows.Forms;
using log4net;
using Microsoft.Win32;

namespace DotNetFrameworkService
{
    static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceToImplement));
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

#if (!DEBUG)
            {
                Log.Info("Notification Service Starting");

                HostFactory.Run(hostFactory =>
                {
                    hostFactory.Service<ServiceToImplement>(notificationService =>
                    {
                        notificationService.ConstructUsing(name => new ServiceToImplement());
                        notificationService.WhenStarted(ns => ns.Start());
                        notificationService.WhenStopped(ns => ns.Stop());
                        notificationService.WhenPaused(ns => ns.Pause());
                        notificationService.WhenContinued(ns => ns.Continue());
                    });

                    hostFactory.SetDescription("Notification Service");
                    hostFactory.SetDisplayName("Notification Service");
                    hostFactory.SetServiceName("Notification Service");

                    hostFactory.OnException(exception =>
                    {
                        Log.Error($"Service Exception: ", exception);
                    });

                    hostFactory.EnableServiceRecovery(serviceRecovery =>
                    {
                        serviceRecovery.RestartService(1);
                        serviceRecovery.OnCrashOnly();
                    });

                    // Turn on Interact With Desktop Flag
                    //https://www.codeproject.com/Articles/4891/Interact-With-Desktop-when-Installing-Windows-Serv
                    string keyPath = @"SYSTEM\CurrentControlSet\Services\Notification Service";
                    RegistryKey RegistryKey = Registry.LocalMachine.OpenSubKey(keyPath, true);

                    if (RegistryKey == null)
                    {
                        Log.Error($"Couldn't find the Registry Key: {keyPath}");
                    }
                    else
                    {
                        Log.Error($"Registry Key Found. ({keyPath})");
                        if (RegistryKey.GetValue("Type") != null)
                        {
                            Log.Error($"Registry Key Path. ({RegistryKey.GetValue("Type")})");
                            RegistryKey.SetValue("Type", ((int)RegistryKey.GetValue("Type") | 256));
                        }
                    }
                });
            }
#else
            Log.Info("Notification Service Debug Session Started");
            NotificationService NotificationService = new NotificationService();
            NotificationService.Debug();
#endif
        }
    }
}
