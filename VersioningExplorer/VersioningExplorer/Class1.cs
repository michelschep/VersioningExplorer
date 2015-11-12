using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace VersioningExplorer
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void test()
        {
//            var deserializedV0 = new DataLoader(0).Load(Messages.V0());
//            var personV0 = (PersonV1)deserializedV0;
//            Assert.That(personV0.Name, Is.EqualTo("joe"), "name");
//
//            var deserializedV1 = new DataLoader(1).Load(Messages.V1());
//            var personV1 = (PersonV2)deserializedV1;
//            Assert.That(personV1.Name, Is.EqualTo("joe"), "name");
//            Assert.That(personV1.Age, Is.EqualTo(21), "age");

            // what if we add a new field to our person
            var deserializedV1 = DataLoader.Load(Messages.V0(), 1);
            var person = (PersonV1)deserializedV1;
            Assert.That(person.Name, Is.EqualTo("joe"), "name");
            Assert.That(person.Age, Is.EqualTo(-1), "age");
        }
    }

    public class PersonV0
    {
        public string Name { get; set; }
    }
    public class PersonV1
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    interface IConvertAJObject
    {
        JObject Apply(JObject from);
    }


    public static class DataLoader
    {
        public static object Load(string messageJson, int targetVersion)
        {
            var containerJObject = (JObject) JsonConvert.DeserializeObject(messageJson);

            // ----- sniffing meta
            var sourceSchema = SchemaOf(containerJObject); // lowest version
            var sourceJObject = containerJObject.Value<JObject>("Payload");

            // ----- upgrade document to current version
            // todo consider type
            var conversion = ConversionFor(sourceSchema.Version, targetVersion);

            var targetJObject = conversion.Apply(sourceJObject);
            
            // ----- actual deserialization to clr type
            // todo consider type
            var targetClrType = ClrTypeFor(targetVersion);

            // what do we need to desarialize the json to a typed object
            return targetJObject.ToObject(targetClrType);
        }

        static IConvertAJObject ConversionFor(int sourceVersion, int targetVersion)
        {
            if (sourceVersion != 0 || targetVersion != 1)
                throw new NotSupportedException($"No conversion for [{sourceVersion}/{targetVersion}]");
            return new ZeroToOnePersonConvertion();
        }

        static Schema SchemaOf(JObject message)
        {
            var typeAlias = message.Value<string>("Schema");
            var version = message.Value<int>("Version");
            return new Schema(typeAlias, version);
        }

        static Type ClrTypeFor(int version)
        {
            if (version == 0)
                return typeof (PersonV0);
            if (version == 1)
                return typeof (PersonV1);

            throw new NotSupportedException($"Unknown type [{version}]");
        }
    }

    class ZeroToOnePersonConvertion : IConvertAJObject
    {
        public JObject Apply(JObject @from)
        {
            @from["Age"] = -1;

            return @from;
        }
    }
}
