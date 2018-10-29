using System;

namespace Flower
{
    public class FlowerFeedEvent
    {
        public string FlowId { get; set; }

        public string StepName { get; set; }

        public DateTime Timestamp { get; set; }

        public FlowerFeedEvent(string flowId, string stepName, DateTime timestamp)
        {
            FlowId = flowId;
            StepName = stepName;
            Timestamp = timestamp;
        }
    }
}
