using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubStatisticsCore.Services
{
    public interface IBackgroundService
    {
        void RunDemoTask();
    }
    public class BackgroundService : IBackgroundService
    {
        public void RunDemoTask()
        {
            Console.WriteLine("its me hangfire");
        }
    }
}
