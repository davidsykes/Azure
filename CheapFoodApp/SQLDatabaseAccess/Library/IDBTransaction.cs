namespace SQLDatabaseAccess.Library
{
    internal interface IDBTransaction
    {
        void Commit();
        void Rollback();
    }
}
