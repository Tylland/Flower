namespace Flower
{
    public interface IFlowerSink
    {
        void Handle(FlowerSeedEvent evt);
        void Handle(FlowerFeedEvent evt);
    }
}
