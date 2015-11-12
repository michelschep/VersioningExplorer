namespace VersioningExplorer.Schema
{
    class SchemaMeta
    {
        public readonly string TypeAlias;
        public readonly int Version;

        public SchemaMeta(string typeAlias, int version)
        {
            TypeAlias = typeAlias;
            Version = version;
        }
    }
}
