using System;
using System.Collections.Generic;

namespace Flower
{
    public class FlowerLogger : IFlower
    {
        private readonly IEnumerable<IFlowerSink> _sinks;
        private readonly Action<string> _selfLogAction;
        public FlowerLogger(IEnumerable<IFlowerSink> sinks, Action<string> selfLogAction)
        {
            _sinks = sinks;
            _selfLogAction = selfLogAction;

            _selfLogAction?.Invoke("FlowerLogger created!");
        }
        public void Seed(string flowId, FlowConfiguration configuration)
        {
            var timestamp = DateTime.Now;

            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Handle(new FlowerSeedEvent(flowId, configuration, timestamp));
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
                    sink.Handle(new FlowerFeedEvent(flowId, stepName, timestamp));
                }
                catch (Exception e)
                {
                    _selfLogAction?.Invoke(e.ToString());
                }
            }
        }
    }
}
