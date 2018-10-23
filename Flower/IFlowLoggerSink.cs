using System;

namespace FlowLogger
{
    public interface IFlowLoggerSink
    {
        void Emit(FlowerSeedEvent evt);
        void Emit(FlowerFeedEvent evt);
    }
}
