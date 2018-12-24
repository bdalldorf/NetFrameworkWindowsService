using log4net;
using Microsoft.Win32;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DotNetFrameworkService
{
    /// <summary>
    /// This is not used. This was the old way before switching to TopShelf
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceToImplement));

        public ProjectInstaller()
        {
            InitializeComponent();
            this.Committed += new InstallEventHandler(ProjectInstaller_Committed);


            // Turn on Interact With Desktop Flag
            //https://www.codeproject.com/Articles/4891/Interact-With-Desktop-when-Installing-Windows-Serv
            string keyPath = $"SYSTEM\\CurrentControlSet\\Services\\{this.serviceInstaller1.ServiceName}";
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
        }

        void ProjectInstaller_Committed(object sender, InstallEventArgs e)
        {
            ServiceController _ServiceController = new ServiceController(this.serviceInstaller1.ServiceName);
            if (_ServiceController.Status == ServiceControllerStatus.Stopped)
                _ServiceController.Start();
        }
    }
}
