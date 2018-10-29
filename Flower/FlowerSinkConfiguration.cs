using System;

namespace Flower
{
    public class FlowerSinkConfiguration
    {
        readonly FlowerConfiguration _loggerConfiguration;
        readonly Action<IFlowerSink> _addSink;

        internal FlowerSinkConfiguration(FlowerConfiguration loggerConfiguration, Action<IFlowerSink> addSink)
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

        public FlowerConfiguration Sink(IFlowerSink sink)
        {
            _addSink(sink);

            return _loggerConfiguration;
        }
    }
}
