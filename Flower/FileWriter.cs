using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Flower
{
    public class FileWriter : LocalWriterBase
    {
        private readonly string _path;

        private readonly Timer _timer;

        public FileWriter(string path, Func<Flow, string> flowRenderFunc = null)
        : base(flowRenderFunc)
        {
            _path = path;

            _timer = new Timer(TimerTick, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
         }

        private void TimerTick(object state)
        {
            lock (Manager)
            {
                Manager.UpdateStatus(DateTime.Now);

                WriteAllFlows();
            }
        }

        private void WriteAllFlows()
        {
            using (var writer = new StreamWriter(_path, false))
            {
                foreach (var line in GetAllFlowLines())
                {
                    writer.WriteLine(line);
                }
            }
        }
        
        private string[] GetAllFlowLines()
        {
            return Manager.Flows.Select(FlowRenderFunc).ToArray();
        }
    }
}
