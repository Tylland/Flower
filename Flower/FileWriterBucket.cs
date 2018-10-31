using System;

namespace Flower
{
    public class FileWriterBucket : IFlowerBucket
    {
        private readonly FileWriter _writer;

        public FileWriterBucket(string path, Func<Flow, string> flowRenderFunc = null)
        {
            _writer = new FileWriter(path, flowRenderFunc);
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
