using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using VersioningExplorer.Schema.Changes;

namespace VersioningExplorer.Schema.Json
{
    class SchemaChangeBasedJsonRewriter : IJsonRewriter
    {
        readonly int _sourceVersion;
        readonly int _targetVersion;
        readonly IDictionary<Type, ISchemaChangeBasedJsonRewriter> _conversions;

        public SchemaChangeBasedJsonRewriter(int sourceVersion, int targetVersion)
        {
            _sourceVersion = sourceVersion;
            _targetVersion = targetVersion;
            _conversions = new Dictionary<Type, ISchemaChangeBasedJsonRewriter>
            {
                {typeof(FieldAdded), new FieldAddedConversion() }
            };
        }

        class FieldAddedConversion : TypedSchemaChangeJsonRewriter<FieldAdded>
        {
            protected override JObject Apply(FieldAdded change, JObject source)
            {
                source[change.FieldName] = new JValue(change.Value);
                return source;
            }
        }

        public JObject Rewrite(JObject json)
        {
            return SchemaChangeEventsBetween(_sourceVersion, _targetVersion).Aggregate(json, Apply);
        }

        JObject Apply(JObject current, SchemaChange change)
        {
            ISchemaChangeBasedJsonRewriter conversion;
            if (!_conversions.TryGetValue(change.GetType(), out conversion))
                throw new NotSupportedException($"schema change event [{change.GetType().FullName}]");
            return conversion.Apply(change, current);
        }

        SchemaChange[] SchemaChangeEventsBetween(int sourceVersion, int targetVersion)
        {
            return new SchemaChange[] {new FieldAdded("Age", -1), };
        }
    }
}