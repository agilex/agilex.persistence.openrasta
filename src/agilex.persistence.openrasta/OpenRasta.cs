using NHibernate;
using OpenRasta.Configuration;
using OpenRasta.DI;
using agilex.persistence.nhibernate;
using agilex.persistence.openrasta.Pipelines;

namespace agilex.persistence.openrasta
{
    public static class AgilexOpenRasta
    {
        public static void ConfigureDatabase(IDatabaseConfigurationParams configurationParams)
        {
            ResourceSpace.Uses.PipelineContributor<RepositoryOpeningPipeline>();
            ResourceSpace.Uses.PipelineContributor<RepositoryClosingPipeline>();
            ResourceSpace.Uses.Resolver.AddDependencyInstance<ISessionFactory>(
                new NhibernateConfiguration().GetSessionFactory(configurationParams), DependencyLifetime.Singleton);
            ResourceSpace.Uses.CustomDependency<IRepositoryFactory, RepositoryFactory>(DependencyLifetime.PerRequest);
        }
    }
}