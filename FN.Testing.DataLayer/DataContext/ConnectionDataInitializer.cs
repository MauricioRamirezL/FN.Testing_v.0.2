using FN.Testing.Common.Common;

namespace FN.Testing.DataLayer.DataContext
{
    public static class ConnectionDataInitializer
    {
        public static void Initialize(ConnectionDataContext context)
        {
            if (StaticConfigs.GetConfig("UseInMemoryDatabase") != "true")
            {
                context.Database.EnsureCreated();
            }            
        }
    }
}
