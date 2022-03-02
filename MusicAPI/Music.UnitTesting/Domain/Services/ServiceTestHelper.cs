using AutoMapper;
using Moq;
using Music.Domain.Contracts.Repositories;
using Music.Domain.Contracts.Services;
using Music.Domain.ErrorHandling.Validations;
using Music.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.UnitTesting.Domain.Services
{
    public class ServiceTestHelper
    {
        public static ArtistService CreateArtistService(Mock<IMapper> mapper = null, Mock<IGenericRepository> moqRepo = null)
        {
            mapper ??= new Mock<IMapper>();
            moqRepo ??= new Mock<IGenericRepository>();
            return new ArtistService(mapper.Object, moqRepo.Object);
            var a = CreateGenericService<ArtistService>(new object[] { mapper.Object, moqRepo.Object });
        }
        public static T CreateGenericService<T>(Object[] objects)
        {
            var constructorParameters = new List<object> { };
            var methodParameters = typeof(T).GetConstructors()[0].GetParameters();
            foreach (var item in methodParameters)
            {
                switch (item.ParameterType.Name)
                {
                    case nameof(IMapper):
                        constructorParameters.Add(((Mock<IMapper>)objects.FirstOrDefault(x => x.GetType().BaseType == typeof(Mock<IMapper>)))?.Object ?? new Mock<IMapper>().Object);
                        break;
                    case nameof(IGenericRepository):
                        constructorParameters.Add(((Mock<IGenericRepository>)objects.FirstOrDefault(x => x.GetType().BaseType == typeof(Mock<IGenericRepository>)))?.Object ?? new Mock<IGenericRepository>().Object);
                        break;
                    case nameof(IEnumerable<Mock<IExternalService>>) + "`1":
                        if (item.ParameterType.GenericTypeArguments[0].Name == nameof(IExternalService))
                        {
                            var defaultExternals = new List<Mock<IExternalService>>() { new Mock<IExternalService>() };
                            var filteredList = objects.Where(x => x.GetType().Name == nameof(List<IExternalService>) + "`1");
                            var mockedExternalService = ((IEnumerable<object>)filteredList.FirstOrDefault()).Cast<object>().ToList().FirstOrDefault();
                            var parameterExternals = new List<Mock<IExternalService>>() { (Mock<IExternalService>)mockedExternalService };
                            if ((parameterExternals?.Any() ?? false))
                                constructorParameters.Add((from it in parameterExternals select it.Object).ToList());
                            else
                                constructorParameters.Add((from it in defaultExternals select it.Object).ToList());
                        }
                        break;
                    case nameof(UserCreationValidator):
                        constructorParameters.Add(((Mock<UserCreationValidator>)objects.FirstOrDefault(x => x.GetType().BaseType == typeof(UserCreationValidator)))?.Object ?? new UserCreationValidator());
                        break;
                    default:
                        constructorParameters.Add(null);
                        break;
                }
            }

            var instance = Activator.CreateInstance(typeof(T), constructorParameters.ToArray());

            return (T)instance;
        }
    }
}
