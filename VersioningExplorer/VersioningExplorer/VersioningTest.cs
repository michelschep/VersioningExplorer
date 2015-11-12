using NUnit.Framework;
using VersioningExplorer.TestSupport;

namespace VersioningExplorer
{
    [TestFixture]
    public class VersioningTest
    {
        [Test]
        public void can_upgrade_object_to_new_version()
        {
            var json = JsonReader.Read(JsonMessages.V0(), 1);

            Assert.That(json["Name"], Is.EqualTo("joe"), "name");
            Assert.That(json["Age"], Is.EqualTo(-1), "age");
        }

        [Test]
        public void can_upgrade_object_multiple_versions()
        {
            var json = JsonReader.Read(JsonMessages.V0(), 2);
            Assert.That(json["Name"], Is.EqualTo("joe"), "name");
            Assert.That(json["Age"], Is.EqualTo(-1), "age");
        }
    }
}
