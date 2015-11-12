using Newtonsoft.Json.Linq;
using VersioningExplorer.Schema.Changes;

namespace VersioningExplorer.Schema.Json
{
    abstract class TypedSchemaChangeJsonRewriter<TChange> : ISchemaChangeBasedJsonRewriter where TChange : SchemaChange
    {
        protected abstract JObject Apply(TChange change, JObject source);

        public JObject Apply(SchemaChange change, JObject source)
        {
            var typedChange = (TChange)change;
            return Apply(typedChange, source);
        }
    }
}
