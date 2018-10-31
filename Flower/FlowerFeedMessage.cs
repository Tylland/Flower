using System;

namespace Flower
{
    public class FlowerFeedMessage
    {
        public string FlowId { get; set; }

        public string StepName { get; set; }

        public DateTime Timestamp { get; set; }

        public FlowerFeedMessage(string flowId, string stepName, DateTime timestamp)
        {
            FlowId = flowId;
            StepName = stepName;
            Timestamp = timestamp;
        }
    }
}
