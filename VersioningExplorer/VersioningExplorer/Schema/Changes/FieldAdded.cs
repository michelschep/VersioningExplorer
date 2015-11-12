namespace VersioningExplorer.Schema.Changes
{
    class FieldAdded : SchemaChange
    {
        public readonly string FieldName;
        public readonly object Value;

        public FieldAdded(string fieldName, object value)
        {
            FieldName = fieldName;
            Value = value;
        }
    }
}