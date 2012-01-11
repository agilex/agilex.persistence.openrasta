using System.Collections.Generic;
using NHibernate;
using OpenRasta.OperationModel;
using OpenRasta.OperationModel.Interceptors;
using OpenRasta.Web;
using agilex.persistence.nhibernate;

namespace agilex.persistence.openrasta
{
    public class RepositoryInterceptor : OperationInterceptor
    {
        readonly ICommunicationContext _context;
        readonly RepositoryFactory _repositoryFactory;

        public const string RepositoryInPiplelineKey = "repo-in-pipeline-key";

        public RepositoryInterceptor(ICommunicationContext context, NHibernateSessionFactoryWrapper sessionFactory)
        {
            _context = context;
            _repositoryFactory = new RepositoryFactory(sessionFactory.SessionFactoryInstance);
        }

        public override bool BeforeExecute(IOperation operation)
        {
            _context.PipelineData.Add(RepositoryInPiplelineKey, _repositoryFactory.Instance());
            return base.BeforeExecute(operation);
        }

        public override bool AfterExecute(IOperation operation, IEnumerable<OutputMember> outputMembers)
        {
            var repository = _context.PipelineData[RepositoryInPiplelineKey] as IRepository;
            if (repository != null) repository.Dispose();
            return base.AfterExecute(operation, outputMembers);
        }
    }
}