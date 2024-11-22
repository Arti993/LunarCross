namespace Infrastructure
{
    public class DIServicesContainer
    {
        private static DIServicesContainer _instance;

        public static DIServicesContainer Instance => _instance ??= new DIServicesContainer();

        public void RegisterService<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService GetService<TService>() where TService : IService => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
