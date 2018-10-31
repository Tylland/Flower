using System;

namespace Flower
{
    public class FlowerBucketConfiguration
    {
        readonly FlowerConfiguration _loggerConfiguration;
        readonly Action<IFlowerBucket> _addBucket;

        internal FlowerBucketConfiguration(FlowerConfiguration loggerConfiguration, Action<IFlowerBucket> addBucket)
        {
            _loggerConfiguration = loggerConfiguration;
            _addBucket = addBucket;
        }

        public FlowerConfiguration Console()
        {
            try
            {
                return Bucket(new ConsoleWriterBucket());
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
                return Bucket(new FileWriterBucket(path));
            }
            catch (Exception ex)
            {
                _loggerConfiguration.SelfLogMessage(ex.ToString());
            }

            return _loggerConfiguration;
        }

        public FlowerConfiguration Bucket(IFlowerBucket bucket)
        {
            _addBucket(bucket);

            return _loggerConfiguration;
        }
    }
}
