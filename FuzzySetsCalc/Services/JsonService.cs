using Newtonsoft.Json;

namespace FuzzySetsCalc.Services
{
    public class JsonService
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public string JsonContentType => "application/json";

        public JsonService(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        public byte[] ToJsonByteArray(object objectToSerialize)
        {
            string json = JsonConvert.SerializeObject(objectToSerialize, _serializerSettings);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            return stream.ToArray();
        }

        public T? FromFormFile<T>(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            var json = reader.ReadToEnd();
            var deserialized = JsonConvert.DeserializeObject<T>(json, _serializerSettings);
            return deserialized;
        }
    }
}
