using SQLiteLibrary;
using SQLLibrary.SQLite;

namespace SQLLibrary.CRUD
{
    internal interface IDatabaseTransactionFactory
    {
        IDatabaseTransaction CreateTransaction(ISQLiteWrapper _sqLiteWrapper);
    }
}
