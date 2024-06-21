using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DashWard.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=(local); Database=DashWardDb; Integrated Security=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection( _connectionString );
        }
    }
}
