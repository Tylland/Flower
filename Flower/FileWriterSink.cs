using System;

namespace Flower
{
    public class FileWriterSink : IFlowerSink
    {
        private readonly FileWriter _writer;

        public FileWriterSink(string path, Func<Flow, string> flowRenderFunc = null)
        {
            _writer = new FileWriter(path, flowRenderFunc);
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
