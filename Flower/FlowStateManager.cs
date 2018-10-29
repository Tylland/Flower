using System;
using System.Collections.Generic;

namespace Flower
{

    public delegate void GenericDelegate<T>(T args);

    public class FlowStarted
    {
        public string SourceName { get; set; }
        public string FlowId { get; set; }
        public string[] EventNames { get; set; }
        public FlowStarted(string flowId, string[] eventNames)
        {
            FlowId = flowId;
            EventNames = eventNames;
        }
    }

    public class FlowCompleted
    {
        public string GroupName { get; set; }
        public string FlowId { get; set; }

        public FlowCompleted(string groupName, string flowId)
        {
            GroupName = groupName;
            FlowId = flowId;
        }
    }

    public class FlowFailed
    {
        public string GroupName { get; set; }
        public string FlowId { get; set; }

        public FlowFailed(string groupName, string flowId)
        {
            GroupName = groupName;
            FlowId = flowId;
        }
    }

    public class FlowFinished
    {
        public string GroupName { get; set; }
        public string FlowId { get; set; }

        public FlowFinished(string groupName, string flowId)
        {
            GroupName = groupName;
            FlowId = flowId;
        }
    }

    public class FlowStepReached
    {
        public string SourceName { get; set; }
        public string FlowId { get; set; }
        public string EventName { get; set; }
        public DateTime Timestamp { get; set; }

        public FlowStepReached(string sourceName, string flowId, string eventName, DateTime timestamp)
        {
            SourceName = sourceName;
            FlowId = flowId;
            EventName = eventName;
            Timestamp = timestamp;
        }
    }

    public class FlowStateManager
    {
        private readonly List<Flow> _flowList = new List<Flow>();
        private readonly Dictionary<string, Flow> _flowDictionary = new Dictionary<string, Flow>();

        public Flow[] Flows => _flowList.ToArray();

        public GenericDelegate<FlowStarted> FlowStarted;
        public GenericDelegate<FlowStepReached> FlowStepReached;
        public GenericDelegate<FlowCompleted> FlowCompleted;
        public GenericDelegate<FlowFailed> FlowFailed;
        public GenericDelegate<FlowFinished> FlowFinished;

        private void AddFlow(Flow flow)
        {
            _flowList.Add(flow);
            _flowDictionary.Add(flow.FlowId, flow);
        }

        public Flow GetFlow(string flowId)
        {
            if(_flowDictionary.ContainsKey(flowId))
                return _flowDictionary[flowId];

            return null;
        }

        private void RemoveFlow(Flow flow)
        {
            _flowList.Remove(flow);
            _flowDictionary.Remove(flow.FlowId);
        }

        public void Start(Flow flow, DateTime timestamp)
        {
            var existing = GetFlow(flow.FlowId);

            if(existing != null)
             RemoveFlow(existing);

            AddFlow(flow);
            flow.Register(new FlowStep(flow.FlowId, flow.Configuration.StepNames[0], timestamp));

            FlowStarted?.Invoke(new FlowStarted(flow.FlowId, flow.Configuration.StepNames));
            FlowStepReached?.Invoke(new FlowStepReached(flow.Configuration.Name, flow.FlowId, flow.Configuration.StepNames[0], timestamp));
        }

        public void Step(string flowId, string stepName, DateTime timestamp)
        {
            var flow = GetFlow(flowId);

            flow.Register(new FlowStep(flowId, stepName, timestamp));

            FlowStepReached?.Invoke(new FlowStepReached(flow.Configuration.Name, flow.FlowId, flow.Configuration.StepNames[0], timestamp));

            if(flow.Completed)
                FlowCompleted?.Invoke(new FlowCompleted(flow.Configuration.Name, flow.FlowId));
        }

        private void UpdateStatus(Flow flow, DateTime now)
        {
            var elapsedTime = now - flow.LastReachedStep.Timestamp;

            if (flow.Completed && !flow.Finished && elapsedTime > flow.Configuration.FinishTimeout)
            {
                flow.Finished = true;
                FlowFinished?.Invoke(new FlowFinished(flow.Configuration.Name, flow.FlowId));
            }
            else if (!flow.Failed && elapsedTime > flow.Configuration.StepTimeout)
            {
                flow.Failed = true;
                FlowFailed?.Invoke(new FlowFailed(flow.Configuration.Name, flow.FlowId));
            }
            else if (!flow.Finished && flow.Failed && elapsedTime > (flow.Configuration.StepTimeout + flow.Configuration.FinishTimeout))
            {
                flow.Finished = true;
                FlowFinished?.Invoke(new FlowFinished(flow.Configuration.Name, flow.FlowId));
            }
        }

        public void UpdateStatus(DateTime now)
        {
            foreach (var flow in Flows)
                UpdateStatus(flow, now);

            foreach (var flow in Flows)
                if(flow.Finished)
                    RemoveFlow(flow);
        }
    }
}
