using SqlSugar;

namespace bookcity.irepository.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISqlSugarClient GetDbClient();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}
