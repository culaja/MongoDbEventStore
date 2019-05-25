using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common
{
    public static class MessageSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Converters = new List<JsonConverter> {new MaybeSerializer<string>()}
        };

        public static string Serialize(this IMessage e) => JsonConvert.SerializeObject(e, Settings);

        public static IMessage Deserialize(this string str) =>
            (IMessage) JsonConvert.DeserializeObject(
                str,
                Settings);
    }
}