using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowLogger
{
    public class Flow
    {
        public string FlowId { get; set; }
        public FlowConfiguration Configuration { get; set; }

        private readonly FlowStep[] _steps;

        public DateTime Starttime { get; set; }

        public double ElapsedTime
        {
            get { return (LastReachedStep.Timestamp - Starttime).TotalMilliseconds; }
        }
        public void Register(FlowStep step)
        {
            foreach (var flowStep in _steps)
            {
                if (flowStep.Name.Equals(step.Name))
                {
                    flowStep.Timestamp = step.Timestamp;
                    break;
                }
            }

        }

        public bool Failed { get; set; }
        public bool Finished { get; set; }
        public bool Completed => _steps.All(step => step.Reached);
        public FlowStep LastStep => _steps.LastOrDefault();
        public FlowStep LastReachedStep => _steps.LastOrDefault(step => step.Reached);
        public FlowStep[] Steps => _steps.ToArray();

        public Flow(string flowId, FlowConfiguration configuration, DateTime starttime)
        {
            FlowId = flowId;
            Configuration = configuration;
            Starttime = starttime;

            _steps = Configuration.StepNames.Select(stepName => new FlowStep(FlowId, stepName)).ToArray();
        }
    }
}
