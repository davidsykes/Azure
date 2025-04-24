namespace SQLLibrary.SQLite
{
    internal interface ICommittable
    {
        void Commit();
        void Rollback();
    }
}
