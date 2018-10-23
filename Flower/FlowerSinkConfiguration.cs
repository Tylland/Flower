using System;

namespace FlowLogger
{
    public class FlowerSinkConfiguration
    {
        readonly FlowerConfiguration _loggerConfiguration;
        readonly Action<IFlowLoggerSink> _addSink;

        internal FlowerSinkConfiguration(FlowerConfiguration loggerConfiguration, Action<IFlowLoggerSink> addSink)
        {
            _loggerConfiguration = loggerConfiguration;
            _addSink = addSink;
        }

        public FlowerConfiguration Console()
        {
            try
            {
                return Sink(new ConsoleWriterSink());
            }
            catch (Exception ex)
            {
                _loggerConfiguration.SelfLogMessage(ex.ToString());
            }

            return _loggerConfiguration;
        }

        public FlowerConfiguration File(string path, Func<Flow, string> flowRenderFunc = null)
        {
            try
            {
                return Sink(new FileWriterSink(path));
            }
            catch (Exception ex)
            {
                _loggerConfiguration.SelfLogMessage(ex.ToString());
            }

            return _loggerConfiguration;
        }

        public FlowerConfiguration Sink(IFlowLoggerSink sink)
        {
            _addSink(sink);

            return _loggerConfiguration;
        }
    }
}
