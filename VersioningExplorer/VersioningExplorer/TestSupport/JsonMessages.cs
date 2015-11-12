using Newtonsoft.Json;

namespace VersioningExplorer.TestSupport
{
    public static class JsonMessages
    {
        public static string V0()
        {
            var json = new
            {
                Schema = "Person", // 'versioned' 'type' of structure
                Version = 0, // 'versioned' 'type' of structure
                Payload = new {
                    Name = "joe"
                }
            };
            return JsonConvert.SerializeObject(json);
        }

        public static string V1()
        {
            var json = new
            {
                Schema = "Person", // 'versioned' 'type' of structure
                Version = 1, // 'versioned' 'type' of structure
                Payload = new
                {
                    Name = "joe",
                    Age = 21
                }
            };
            return JsonConvert.SerializeObject(json);
        }
    }
}