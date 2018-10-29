using System;

namespace Flower
{
    public class FlowConfiguration
    {
        public FlowConfiguration(string[] stepNames, TimeSpan stepTimeout)
        {
            StepNames = stepNames;
            StepTimeout = stepTimeout;
        }

        public FlowConfiguration()
        {
            
        }

        public string Name { get; set; }
        public TimeSpan FinishTimeout { get; set; }
        public TimeSpan StepTimeout { get; set; }
        public string[] StepNames { get; set; }

        public FlowConfiguration WithSteps(string[] stepNames)
        {
            StepNames = stepNames;

            return this;
        }

    }
}
