using Newtonsoft.Json.Linq;
using VersioningExplorer.Schema.Changes;

namespace VersioningExplorer.Schema.Json
{
    interface ISchemaChangeBasedJsonRewriter
    {
        JObject Apply(SchemaChange change, JObject source);
    }
}
