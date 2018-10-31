using System;

namespace Flower
{
    public class ConsoleWriterBucket : IFlowerBucket
    {
        private readonly ConsoleWriter _writer;
        public ConsoleWriterBucket(Func<Flow, string> flowRenderFunc = null)
        {
            _writer = new ConsoleWriter(flowRenderFunc);
        }
        public void Handle(FlowerSeedMessage evt)
        {
            _writer.Seed(evt.FlowId, evt.Configuration, evt.Timestamp);
        }
        public void Handle(FlowerFeedMessage evt)
        {
            _writer.Feed(evt.FlowId, evt.StepName, evt.Timestamp);
        }
    }
}
