using System;
using Newtonsoft.Json.Linq;

namespace VersioningExplorer.Serialization
{
    static internal class JsonSerializer
    {
        static object DeserializeToClr(int targetVersion, JObject targetJObject)
        {
            // ----- actual deserialization to clr type
            // todo consider type
            var targetClrType = ClrTypeFor(targetVersion);

            // what do we need to desarialize the json to a typed object
            return targetJObject.ToObject(targetClrType);
        }

        static Type ClrTypeFor(int version)
        {
            if (version == 0)
                return typeof(PersonV0);
            if (version == 1)
                return typeof(PersonV1);

            throw new NotSupportedException($"Unknown type [{version}]");
        }
    }
}