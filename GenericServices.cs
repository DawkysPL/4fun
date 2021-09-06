using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using static ConsoleApp3.GenericServices;

namespace ConsoleApp3
{
    public class GenericServices
    {
        private ServiceCollection serviceCollection;
        private ServiceProvider serviceProvider;
        public readonly IGenericProvider _provider;
        public GenericServices()
        {
            serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IGenericProvider, GenericProvider>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Act()
        {
            var xdd = typeof(CarService);
            var hehe = ServiceTypes.Plane.ToName();
            var factor = new GenericServiceFactory(serviceProvider.GetService<IGenericProvider>());
            var service = factor.CreateService(xdd);
        }

        public class GenericService
        {
            public readonly IGenericProvider _provider;
            public GenericService()
            {

            }
        }

        public class GenericServiceFactory
        {
            IServiceProvider serviceProvider;
            public readonly IGenericProvider _provider;

            public GenericServiceFactory(IGenericProvider provider)
            {
                _provider = provider;
            }


            public GenericService CreateService(Type type)
            {
                _provider.Print($"To ja - twoj provider: {type.Name}");

                return (GenericService)typeof(GenericServiceFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.Name == nameof(CreateService) && x.IsGenericMethod)
                    .Single()
                    .MakeGenericMethod(type)
                    .Invoke(this, new object[] { 1 });
            }

            private T CreateService<T>(int a) where T : GenericService
            {
                return ActivatorUtilities.CreateInstance(serviceProvider, typeof(T), a) as T;
            }
        }

        public class CarService : GenericService
        {
            public CarService(int a)
            {
                Console.WriteLine($"CarService {a}");
            }
        }

        public class TruckService : GenericService
        {
            public TruckService(int a)
            {
                Console.WriteLine($"TruckService {a}");
            }
        }

        public class PlaneService : GenericService
        {
            public PlaneService(int a)
            {
                Console.WriteLine($"PlaneService {a}");
            }
        }

        public class GenericProvider : IGenericProvider
        {
            public void Print(string text)
            {
                Console.WriteLine(text);
            }
        }

        public interface IGenericProvider
        {
            void Print(string text);
        }

        public enum ServiceTypes
        {
            [AtributeHelper(Type = typeof(CarService))]
            Car = 1,
            [AtributeHelper(Type = typeof(TruckService))]
            Truck = 2,
            [AtributeHelper(Type = typeof(PlaneService))]
            Plane = 3
        }

        public class AtributeHelper : Attribute
        {
            public Type Type { get; set; }
        }


    }

    public static class EnumExtensions
    {

        // This extension method is broken out so you can use a similar pattern with 
        // other MetaData elements in the future. This is your base method for each.
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
                ? (T)attributes[0]
                : null;
        }

        // This method creates a specific call to the above method, requesting the
        // Description MetaData attribute.
        public static Type ToName(this Enum value)
        {
            var attribute = value.GetAttribute<AtributeHelper>();
            return attribute == null ?
                throw new Exception("Brak atrybutu")
                : attribute.Type;
        }
    }
}
