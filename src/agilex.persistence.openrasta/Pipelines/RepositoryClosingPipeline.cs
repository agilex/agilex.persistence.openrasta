using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace agilex.persistence.openrasta.Pipelines
{
    public class RepositoryClosingPipeline : IPipelineContributor
    {
        #region IPipelineContributor Members

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(Close).Before<KnownStages.IResponseCoding>();
        }

        #endregion

        public PipelineContinuation Close(ICommunicationContext context)
        {
            if (context.Request.Uri.ToString().Contains("favicon")) return PipelineContinuation.Continue;

            var repository = context.PipelineData[ContextKeys.Repository] as IRepository;
            if (repository != null) repository.Dispose();
            return PipelineContinuation.Continue;
        }
    }
}