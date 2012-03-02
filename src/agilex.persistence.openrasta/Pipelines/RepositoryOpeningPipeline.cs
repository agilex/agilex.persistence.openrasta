using OpenRasta.Pipeline;
using OpenRasta.Web;
using agilex.persistence.Repository.Callbacks;

namespace agilex.persistence.openrasta.Pipelines
{
    public class RepositoryOpeningPipeline : IPipelineContributor
    {
        readonly IRepositoryCallbacks _repositoryCallbacks;
        readonly IRepositoryFactory _repositoryFactory;

        public RepositoryOpeningPipeline(IRepositoryFactory repositoryFactory) : this(repositoryFactory, null)
        {
        }

        public RepositoryOpeningPipeline(IRepositoryFactory repositoryFactory, IRepositoryCallbacks repositoryCallbacks)
        {
            _repositoryFactory = repositoryFactory;
            _repositoryCallbacks = repositoryCallbacks;
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

            IRepository repository = _repositoryCallbacks == null
                                         ? _repositoryFactory.Instance()
                                         : _repositoryFactory.Instance(_repositoryCallbacks);
            repository.BeginTransaction();
            context.PipelineData.Add(ContextKeys.Repository, repository);
            return PipelineContinuation.Continue;
        }
    }
}