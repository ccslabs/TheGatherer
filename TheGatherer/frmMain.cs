using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGatherer
{
    public partial class frmMain : Form
    {

        private double seconds = 0;
        private double previousSeconds = 0;

        private bool PauseOperations = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            previousSeconds = Properties.Settings.Default.PreviousRuntime;
            lblVersion.Text = CoreAssembly.Version.ToString();

           
        }

        private void RunThread()
        {
            while(!PauseOperations)
            {
                string CurrentURL = GetURLToProcess(); // Get Next URL from the Database
            }
        }

        private string GetURLToProcess()
        {
            throw new NotImplementedException();
        }



        private void timerOneSecond_Tick(object sender, EventArgs e)
        {
            seconds++;
            TimeSpan thisSessionRuntime = TimeSpan.FromSeconds(seconds);
            TimeSpan totalRuntime = TimeSpan.FromSeconds(seconds + previousSeconds);

            lblRunTimeThisSession.Text = thisSessionRuntime.ToString();
            lblRunTimeTotal.Text = totalRuntime.ToString();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PreviousRuntime = previousSeconds + seconds;
            Properties.Settings.Default.Save();
        }

        private void btnStartPause_Click(object sender, EventArgs e)
        {
            if(btnStartPause.Text == "Start")
            {
                PauseOperations = false;
                btnStartPause.Text = "Pause";
                // kick the thread into action
                Task.Run(() => RunThread());
            }
            else
            {
                btnStartPause.Text = "Start";
                PauseOperations = true;
            }
        }
    }

    public static class CoreAssembly
    {
        public static readonly Assembly Reference = typeof(CoreAssembly).Assembly;
        public static readonly Version Version = Reference.GetName().Version;
    }
}
