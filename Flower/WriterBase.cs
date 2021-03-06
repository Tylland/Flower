﻿using System;
using System.Linq;

namespace Flower
{
    /// <summary>
    /// Base class for local writers
    /// </summary>
    public abstract class LocalWriterBase
    {
        private const int HeaderWidth = 15;
        private const int ProgressBarWidth = 50;

        private readonly FlowStateManager _manager = new FlowStateManager();
        /// <summary>
        /// 
        /// </summary>
        protected readonly Func<Flow, string> FlowRenderFunc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowRenderFunc"></param>
        protected LocalWriterBase(Func<Flow, string> flowRenderFunc)
        {
            FlowRenderFunc = flowRenderFunc ?? CreateFlowString;

            _manager.FlowStarted += OnFlowStarted;
            _manager.FlowStepReached += OnFlowStepReached;
            _manager.FlowCompleted += OnFlowCompleted;
            _manager.FlowFailed += OnFlowFailed;
            _manager.FlowFinished += OnFlowFinished;
        }

        /// <summary>
        /// 
        /// </summary>
        protected FlowStateManager Manager => _manager;
        protected virtual void OnFlowStarted(FlowStarted args) { }
        protected virtual void OnFlowFinished(FlowFinished args) { }
        protected virtual void OnFlowFailed(FlowFailed args) { }
        protected virtual void OnFlowCompleted(FlowCompleted args) { }
        protected virtual void OnFlowStepReached(FlowStepReached args) { }

        /// <summary>
        /// Creates a string that visualizes the state of a flow
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        protected string CreateFlowString(Flow flow)
        {
            var header = (flow.FlowId + ":").PadLeft(HeaderWidth, ' ');

            var stepWidth = ProgressBarWidth / flow.Steps.Length;

            var progressSteps =
                "[" + string.Join("", flow.Steps.Select(step => ">".PadLeft(stepWidth, step.Reached ? '=' : ' '))) +
                "] - " + flow.LastReachedStep.Name + " - " + flow.ElapsedTime + " ms";

            return $"{header}{progressSteps}";
        }

        /// <summary>
        /// Initializes a new flow
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="configuration"></param>
        /// <param name="timestamp"></param>
        public void Seed(string flowId, FlowConfiguration configuration, DateTime timestamp)
        {
            _manager.Start(new Flow(flowId, configuration, timestamp), timestamp);
        }
        
        /// <summary>
        /// Updates the state of a flow
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="stepName"></param>
        /// <param name="timestamp"></param>
        public void Feed(string flowId, string stepName, DateTime timestamp)
        {
            _manager.Step(flowId, stepName, timestamp);
        }
    }
}
