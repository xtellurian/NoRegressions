namespace core.Contract
{
    public interface ITargetFactory
    {
        T CreateTarget<T>() where T: class;
    }
}