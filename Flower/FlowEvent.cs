using System;

namespace FlowLogger
{
    public class FlowStep
    {
        private static readonly DateTime NoTimestamp = DateTime.MinValue;
        public string FlowId { get; set; }
        public string Name { get; set; }
        public bool Reached => !Timestamp.Equals(NoTimestamp);
        public DateTime Timestamp { get; set; }

        public FlowStep(string flowId, string name, DateTime timestamp)
        {
            FlowId = flowId;
            Name = name;
            Timestamp = timestamp;
        }
        public FlowStep(string flowId, string name)
        : this(flowId, name, NoTimestamp)
        {
        }
    }
}
