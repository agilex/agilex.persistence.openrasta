using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace agilex.persistence.openrasta.Pipelines
{
    public class RepositoryOpeningPipeline : IPipelineContributor
    {
        readonly IRepositoryFactory _repositoryFactory;

        public RepositoryOpeningPipeline(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        #region IPipelineContributor Members

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(OpenRepo).Before<KnownStages.IAuthentication>();
        }

        #endregion

        public PipelineContinuation OpenRepo(ICommunicationContext context)
        {
            if (context.Request.Uri.ToString().Contains("favicon")) return PipelineContinuation.Continue;

            IRepository repository = _repositoryFactory.Instance();
            repository.BeginTransaction();
            context.PipelineData.Add(ContextKeys.Repository, repository);
            return PipelineContinuation.Continue;
        }
    }
}