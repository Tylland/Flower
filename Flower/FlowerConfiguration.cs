using System;
using System.Collections.Generic;

namespace Flower
{
    public class FlowerConfiguration
    {
        private readonly List<IFlowerSink> _logEventSinks = new List<IFlowerSink>();
        private Action<string> _selfLogAction;
        public FlowerConfiguration()
        {
            WriteTo = new FlowerSinkConfiguration(this, s => _logEventSinks.Add(s));
        }
        public FlowerSinkConfiguration WriteTo { get; internal set; }

        internal void SelfLogMessage(string msg)
        {
            _selfLogAction?.Invoke(msg);
        }

        public FlowerConfiguration SelfLog(Action<string> selfLogAction)
        {
            _selfLogAction = selfLogAction;

            return this;
        }

        public IFlower CreateLogger()
        {
            Flower.Logger = new FlowerLogger(_logEventSinks, _selfLogAction);

            return Flower.Logger;
        }
    }
}
