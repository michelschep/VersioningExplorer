using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VersioningExplorer.Schema;
using VersioningExplorer.Schema.Json;

namespace VersioningExplorer
{
    public static class JsonReader
    {
        public static JObject Read(string messageJson, int targetVersion)
        {
            var containerJObject = (JObject)JsonConvert.DeserializeObject(messageJson);

            // ----- sniffing meta
            var sourceSchema = SchemaOf(containerJObject); // lowest version
            var sourceJson = containerJObject.Value<JObject>("Payload");

            // ----- upgrade document to current version
            // todo consider type
            var rewriter = RewriterFor(sourceSchema.Version, targetVersion);

            return rewriter.Rewrite(sourceJson);
        }

        static IJsonRewriter RewriterFor(int sourceVersion, int targetVersion)
        {
            //            if (sourceVersion != 0 || targetVersion != 1)
            //                throw new NotSupportedException($"No conversion for [{sourceVersion}/{targetVersion}]");


            return new SchemaChangeBasedJsonRewriter(sourceVersion, targetVersion);
        }

        static SchemaMeta SchemaOf(JObject message)
        {
            var typeAlias = message.Value<string>("Schema");
            var version = message.Value<int>("Version");
            return new SchemaMeta(typeAlias, version);
        }
    }
}