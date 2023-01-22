using Autofac;
using OpenXrm.Core.Database.Infrastructure;

namespace OpenXrm.Core.Database
{
  public class DatabaseModule: Module
    {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<CoreContext>();
        }
    }
}
