namespace VersioningExplorer
{
    class Schema
    {
        public readonly string TypeAlias;
        public readonly int Version;

        public Schema(string typeAlias, int version)
        {
            TypeAlias = typeAlias;
            Version = version;
        }
    }
}