using OpenRasta.Web;
using agilex.persistence.openrasta.Pipelines;

namespace agilex.persistence.openrasta
{
    public class OpenRastaRepositoryLocator : IRepositoryLocator
    {
        readonly IRepository _repository;

        public OpenRastaRepositoryLocator(ICommunicationContext communicationContext)
        {
            _repository = communicationContext.PipelineData[ContextKeys.Repository] as IRepository;
        }

        public IRepository RepositoryInstance
        {
            get { return _repository; }
        }
    }
}