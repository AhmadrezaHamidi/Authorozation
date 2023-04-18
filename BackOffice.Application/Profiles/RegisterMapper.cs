using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Profiles
{
    public class RegisterMapper : Profile
    {
        public RegisterMapper()
        {
            var assemblies = new List<Assembly>();

            var applicationAssembly = Assembly.GetExecutingAssembly();
            var webApiAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Ticketing.WebApi"));
            assemblies.Add(applicationAssembly);
            assemblies.AddRange(webApiAssembly);
            ApplyMappingProfiles(assemblies);
        }
        private void ApplyMappingProfiles(IEnumerable<Assembly> assemblies)
        {
            foreach (var item in assemblies)
            {
                var types = item.GetExportedTypes().Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreateMapper<>)))
                    .ToList();

                foreach (var type in types)
                {
                    var model = Activator.CreateInstance(type);

                    var methodInfo = type.GetMethod("Map") //get the map method directly by the class
                                     ?? type.GetInterface("ICreateMapper`1").GetMethod("Map"); //if null get the interface implementation

                    if (model != null)
                        methodInfo?.Invoke(model, new object[] { this });
                }
            }

        }
    }
}
