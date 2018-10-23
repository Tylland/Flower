using System;
using System.Linq;

namespace FlowLogger
{
    public class WriterBase
    {
        private const int HeaderWidth = 15;
        private const int ProgressBarWidth = 50;

        private readonly FlowStateManager _manager = new FlowStateManager();
        protected readonly Func<Flow, string> FlowRenderFunc;

        public WriterBase(Func<Flow, string> flowRenderFunc)
        {
            FlowRenderFunc = flowRenderFunc ?? CreateFlowString;

            _manager.FlowStarted += OnFlowStarted;
            _manager.FlowStepReached += OnFlowStepReached;
            _manager.FlowCompleted += OnFlowCompleted;
            _manager.FlowFailed += OnFlowFailed;
            _manager.FlowFinished += OnFlowFinished;
        }

        protected FlowStateManager Manager => _manager;
        protected virtual void OnFlowStarted(FlowStarted args) { }
        protected virtual void OnFlowFinished(FlowFinished args) { }
        protected virtual void OnFlowFailed(FlowFailed args) { }
        protected virtual void OnFlowCompleted(FlowCompleted args) { }
        protected virtual void OnFlowStepReached(FlowStepReached args) { }

        protected string CreateFlowString(Flow flow)
        {
            var header = (flow.FlowId + ":").PadLeft(HeaderWidth, ' ');

            var stepWidth = ProgressBarWidth / flow.Steps.Length;

            var progressSteps =
                "[" + string.Join("", flow.Steps.Select(step => ">".PadLeft(stepWidth, step.Reached ? '=' : ' '))) +
                "] - " + flow.LastReachedStep.Name + " - " + flow.ElapsedTime + " ms";

            return $"{header}{progressSteps}";
        }

        public void Start(string flowId, FlowConfiguration configuration, DateTime timestamp)
        {
            _manager.Start(new Flow(flowId, configuration, timestamp), timestamp);
        }

        public void Step(string flowId, string stepName, DateTime timestamp)
        {
            _manager.Step(flowId, stepName, timestamp);
        }
    }
}
