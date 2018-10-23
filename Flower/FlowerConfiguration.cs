using System;
using System.Collections.Generic;

namespace FlowLogger
{
    public class FlowerConfiguration
    {
        private readonly List<IFlowLoggerSink> _logEventSinks = new List<IFlowLoggerSink>();
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

        public IFlowLogger CreateLogger()
        {
            Flower.Logger = new FlowLogger(_logEventSinks, _selfLogAction);

            return Flower.Logger;
        }
    }
}
