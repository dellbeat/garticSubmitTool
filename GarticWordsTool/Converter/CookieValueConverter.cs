using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GarticWordsTool.Converter
{
    public class CookieValueConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = default(string);
            try
            {
                value = reader.GetString()?.Replace(Environment.NewLine, "");
            }
            catch
            {

            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
