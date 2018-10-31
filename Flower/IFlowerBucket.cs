namespace Flower
{
    public interface IFlowerBucket
    {
        void Handle(FlowerSeedMessage evt);
        void Handle(FlowerFeedMessage evt);
    }
}
