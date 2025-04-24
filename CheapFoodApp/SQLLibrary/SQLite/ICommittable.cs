namespace SQLiteLibrary.SQLite
{
    internal interface ICommittable
    {
        void Commit();
        void Rollback();
    }
}
