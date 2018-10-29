using System;

namespace Flower
{
    public class ConsoleWriterSink : IFlowerSink
    {
        private readonly ConsoleWriter _writer;
        public ConsoleWriterSink(Func<Flow, string> flowRenderFunc = null)
        {
            _writer = new ConsoleWriter(flowRenderFunc);
        }
        public void Handle(FlowerSeedEvent evt)
        {
            _writer.Seed(evt.FlowId, evt.Configuration, evt.Timestamp);
        }
        public void Handle(FlowerFeedEvent evt)
        {
            _writer.Feed(evt.FlowId, evt.StepName, evt.Timestamp);
        }
    }
}
