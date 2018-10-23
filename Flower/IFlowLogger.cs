namespace FlowLogger
{
    public interface IFlowLogger
    {
        void Seed(string flowId, FlowConfiguration configuration);
        void Feed(string flowId, string stepName);
    }
}
