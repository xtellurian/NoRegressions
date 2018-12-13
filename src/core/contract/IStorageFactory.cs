namespace core.Contract
{
    public interface IStorageFactory
    {
        T CreateCloudProvider<T>() where T: class;
    }
}