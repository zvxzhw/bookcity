using bookcity.irepository.UnitOfWork;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookcity.repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISqlSugarClient _sqlSugarClient;

        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        public void BeginTran()
        {
            _sqlSugarClient.Ado.BeginTran();
        }

        public void CommitTran()
        {
            _sqlSugarClient.Ado.CommitTran();
        }

        public ISqlSugarClient GetDbClient()
        {
            return _sqlSugarClient;
        }

        public void RollbackTran()
        {
            _sqlSugarClient.Ado.RollbackTran();
        }
    }
}
