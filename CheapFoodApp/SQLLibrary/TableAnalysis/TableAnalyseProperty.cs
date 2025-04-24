namespace SQLLibrary.TableAnalysis
{
    internal class TableAnalyseProperty
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; internal set; } = string.Empty;
        public bool Nullable { get; set; } = false;
    }
}
