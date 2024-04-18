public class AllServicesContainer
{
    private static AllServicesContainer _instance;

    public static AllServicesContainer Instance => _instance ??= new AllServicesContainer();

    public void RegisterService<TService>(TService implementation) where TService : IService =>
        Implementation<TService>.ServiceInstance = implementation;

    public TService GetService<TService>() where TService : IService => Implementation<TService>.ServiceInstance;

    private static class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}
