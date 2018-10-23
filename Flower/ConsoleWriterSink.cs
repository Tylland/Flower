using System;

namespace FlowLogger
{
    public class ConsoleWriterSink : IFlowLoggerSink
    {
        private readonly ConsoleWriter _writer;
        public ConsoleWriterSink(Func<Flow, string> flowRenderFunc = null)
        {
            _writer = new ConsoleWriter(flowRenderFunc);
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
