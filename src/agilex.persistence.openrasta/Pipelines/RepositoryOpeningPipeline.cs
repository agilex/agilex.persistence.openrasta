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
            pipelineRunner.Notify(OpenRepo).Before<KnownStages.IBegin>();
        }

        #endregion

        public PipelineContinuation OpenRepo(ICommunicationContext context)
        {
            IRepository repository = _repositoryFactory.Instance();
            repository.BeginTransaction();
            context.PipelineData.Add(ContextKeys.Repository, repository);
            return PipelineContinuation.Continue;
        }
    }
}