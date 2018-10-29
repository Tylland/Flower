using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Flower
{
    public class ConsoleWriter :  LocalWriterBase
    {
        private readonly List<string> _activeFlows = new List<string>();
        private readonly Timer _timer;

        private void UpdateStatus(object state)
        {
            Manager.UpdateStatus(DateTime.Now);

            //RenderAllFlows();
        }

        public ConsoleWriter(Func<Flow, string> flowRenderFunc)
        : base(flowRenderFunc)
        {
            try
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to access the console!", ex);
            }

            _timer = new Timer(UpdateStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

 
        }

        protected override void OnFlowFinished(FlowFinished args)
        {
            RenderFlow(args.FlowId);
        }

        protected override void OnFlowFailed(FlowFailed args)
        {
            RenderFlow(args.FlowId);
        }

        protected override void OnFlowCompleted(FlowCompleted args)
        {
            RenderFlow(args.FlowId);
        }

        protected override void OnFlowStepReached(FlowStepReached args)
        {
            RenderFlow(args.FlowId);
        }

        protected override void OnFlowStarted(FlowStarted args)
        {
            RenderFlow(args.FlowId);
            _activeFlows.Add(args.FlowId);
        }
        
        private void RenderFlow(string flowId)
        {
            var flow = Manager.GetFlow(flowId);
            var flowIndex = _activeFlows.IndexOf(flowId);


            if (flow.Completed)
                Console.ForegroundColor = ConsoleColor.Green;
            if (flow.Failed)
                Console.ForegroundColor = ConsoleColor.Red;

            if (flowIndex > -1)
                Console.SetCursorPosition(0, flowIndex);

            Console.WriteLine(CreateFlowString(flow).PadRight(Console.WindowWidth, ' '));

            Console.ResetColor();
        }

    }
}
