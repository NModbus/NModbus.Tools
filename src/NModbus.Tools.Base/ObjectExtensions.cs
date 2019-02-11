using Newtonsoft.Json;

namespace NModbus.Tools.Base
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs a deep clone of an object via JSON serialization / deserialziation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            var json = JsonConvert.SerializeObject(source, settings);

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
