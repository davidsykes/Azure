namespace SQLLibrary.TableAnalysis
{
    internal class TableAnalyserCache : ITableAnalyser
    {
        private ITableAnalyser _tableAnalyser;
        private readonly Dictionary<string, IAnalysedTable> _details;

        public TableAnalyserCache(ITableAnalyser tableAnalyser)
        {
            _tableAnalyser = tableAnalyser;
            _details = new Dictionary<string, IAnalysedTable>();
        }

        public IAnalysedTable AnalyseTable<TDBType>()
        {
            var typeName = typeof(TDBType).Name;

            if (_details.TryGetValue(typeName, out IAnalysedTable? cachedDetails))
            {
                return cachedDetails;
            }

            var details = _tableAnalyser.AnalyseTable<TDBType>();
            _details.Add(typeName, details);
            return details;
        }
    }
}
