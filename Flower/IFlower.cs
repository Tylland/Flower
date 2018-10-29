namespace Flower
{
    public interface IFlower
    {
        void Seed(string flowId, FlowConfiguration configuration);
        void Feed(string flowId, string stepName);
    }
}
