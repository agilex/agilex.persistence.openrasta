using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using agilex.persistence.nhibernate;

namespace agilex.persistence.openrasta
{
    public class NHibernateSessionFactoryWrapper
    {
        readonly IDatabaseConfigurationParams _configuration;
        static ISessionFactory _sessionFactory;     

        public NHibernateSessionFactoryWrapper(IDatabaseConfigurationParams configuration)
        {
            _configuration = configuration;
        }

        public ISessionFactory SessionFactoryInstance
        {
            get
            {
                return _sessionFactory ??
                       (_sessionFactory = new NhibernateConfiguration().GetSessionFactory(_configuration));
            }
        }
    }
}