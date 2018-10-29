using System;

namespace Flower
{
    public class FlowerSeedEvent
    {
        public string FlowId { get; set; }

        public FlowConfiguration Configuration { get; set; }

        public DateTime Timestamp { get; set; }

        public FlowerSeedEvent(string flowId, FlowConfiguration configuration, DateTime timestamp)
        {
            FlowId = flowId;
            Configuration = configuration;
            Timestamp = timestamp;
        }
    }
}
