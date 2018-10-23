using System;
using System.Collections.Generic;

namespace FlowLogger
{
    public class FlowLogger : IFlowLogger
    {
        private readonly IEnumerable<IFlowLoggerSink> _sinks;
        private readonly Action<string> _selfLogAction;
        public FlowLogger(IEnumerable<IFlowLoggerSink> sinks, Action<string> selfLogAction)
        {
            _sinks = sinks;
            _selfLogAction = selfLogAction;

            _selfLogAction?.Invoke("FlowLogger created!");
        }
        public void Seed(string flowId, FlowConfiguration configuration)
        {
            var timestamp = DateTime.Now;

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Emit(new FlowerSeedEvent(flowId, configuration, timestamp));
                }
                catch (Exception e)
                {
                    _selfLogAction?.Invoke(e.ToString());
                }
            }
        }
        public void Feed(string flowId, string stepName)
        {
            var timestamp = DateTime.Now;

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Emit(new FlowerFeedEvent(flowId, stepName, timestamp));
                }
                catch (Exception e)
                {
                    _selfLogAction?.Invoke(e.ToString());
                }
            }
        }
    }
}
