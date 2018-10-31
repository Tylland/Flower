using System;
using System.Collections.Generic;

namespace Flower
{
    public class FlowerConfiguration
    {
        private readonly List<IFlowerBucket> _buckets = new List<IFlowerBucket>();
        private Action<string> _selfLogAction;
        public FlowerConfiguration()
        {
            WriteTo = new FlowerBucketConfiguration(this, s => _buckets.Add(s));
        }
        public FlowerBucketConfiguration WriteTo { get; internal set; }

        internal void SelfLogMessage(string msg)
        {
            _selfLogAction?.Invoke(msg);
        }

        public FlowerConfiguration SelfLog(Action<string> selfLogAction)
        {
            _selfLogAction = selfLogAction;

            return this;
        }

        public IFlower CreateFlower()
        {
            Flower.Logger = new FlowerLogger(_buckets, _selfLogAction);

            return Flower.Logger;
        }
    }
}
