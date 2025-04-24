namespace SQLLibrary.TableAnalysis
{
    internal interface IAnalysedTable
    {
        public string ClassName { get; }
        public string TableName { get; }
        public IList<TableAnalyseProperty> Properties { get; }
        public bool PrimaryKeyIsAutoIncrement { get; }
        public IList<string> PrimaryKeys { get; }
        public string CreateTableQuery { get; }
        public string InsertQuery { get; }
        public string UpsertQuery1 { get; }
        public string UpsertQuery2 { get; }
        public string SelectQuery { get; }
        public string UpdateQuery { get; }
        public string DeleteQueryBase { get; }
    }
}