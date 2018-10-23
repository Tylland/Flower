using System;

namespace FlowLogger
{
    public class FileWriterSink : IFlowLoggerSink
    {
        private readonly FileWriter _writer;

        public FileWriterSink(string path, Func<Flow, string> flowRenderFunc = null)
        {
            _writer = new FileWriter(path, flowRenderFunc);
        }

        public void Emit(FlowerSeedEvent evt)
        {
            _writer.Start(evt.FlowId, evt.Configuration, evt.Timestamp);
        }

        public void Emit(FlowerFeedEvent evt)
        {
            _writer.Step(evt.FlowId, evt.StepName, evt.Timestamp);
        }
    }
}
