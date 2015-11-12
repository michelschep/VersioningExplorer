using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace VersioningExplorer
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void test()
        {
            var serializedMessageV1 = "{'name': 'rob'}";

            var serializer = new OurSerializer();

            var deserializedMessageV1 = serializer.Deserialize(serializedMessageV1);

            Assert.That(deserializedMessageV1.Name, Is.EqualTo("rob"), "name of person");
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class OurSerializer
    {
        public Person Deserialize(string serializedMessageV1)
        {
            return JsonConvert.DeserializeObject<Person>(serializedMessageV1);
        }
    }
}
