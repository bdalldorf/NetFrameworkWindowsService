using log4net;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace DotNetFrameworkService
{
    public class ServiceToImplement : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceToImplement));

        public ServiceToImplement()
        {

        }

        internal void Debug()
        {
        }

        private void sender(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnStart(string[] args)
        {
            Log.Info("OnStart() Method Called");
        }

        public void Start()
        {
            Log.Info("Start() Method Called");
        }

        protected override void OnStop()
        {
            Log.Info("OnStop() Method Called");
        }

        public new void Stop()
        {
            Log.Info("Stop() Method Called");
        }

        protected override void OnContinue()
        {
            Log.Info("OnContinue() Method Called");
        }

        public void Continue()
        {
            Log.Info("Continue() Method Called");
        }

        protected override void OnPause()
        {
            Log.Info("OnPause() Method Called");
        }

        public void Pause()
        {
            Log.Info("Pause Method Called");
        }
    }
}
