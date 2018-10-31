using System;
using System.Collections.Generic;

namespace Flower
{
    public class FlowerLogger : IFlower
    {
        private readonly IEnumerable<IFlowerBucket> _buckets;
        private readonly Action<string> _selfLogAction;
        public FlowerLogger(IEnumerable<IFlowerBucket> buckets, Action<string> selfLogAction)
        {
            _buckets = buckets;
            _selfLogAction = selfLogAction;

            _selfLogAction?.Invoke("FlowerLogger created!");
        }
        public void Seed(string flowId, FlowConfiguration configuration)
        {
            var timestamp = DateTime.Now;

            foreach (var bucket in _buckets)
            {
                try
                {
                    bucket.Handle(new FlowerSeedMessage(flowId, configuration, timestamp));
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

            foreach (var bucket in _buckets)
            {
                try
                {
                    bucket.Handle(new FlowerFeedMessage(flowId, stepName, timestamp));
                }
                catch (Exception e)
                {
                    _selfLogAction?.Invoke(e.ToString());
                }
            }
        }
    }
}
