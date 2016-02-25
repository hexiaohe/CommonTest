using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

namespace WindowsServiceTest
{
    public partial class Service1 : ServiceBase
    {
        private IScheduler scheduler;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();
        }

        protected override void OnStop()
        {
            scheduler.Shutdown();
        }
    }
}
