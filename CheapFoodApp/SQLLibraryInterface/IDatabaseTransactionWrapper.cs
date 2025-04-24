namespace SQLLibraryInterface
{
    public interface IDatabaseTransactionWrapper
    {
        void Commit();
        void Rollback();
    }
}
