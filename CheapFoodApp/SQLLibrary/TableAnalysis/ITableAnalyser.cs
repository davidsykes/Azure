namespace SQLLibrary.TableAnalysis
{
    internal interface ITableAnalyser
    {
        IAnalysedTable AnalyseTable<TDBType>();
    }
}
