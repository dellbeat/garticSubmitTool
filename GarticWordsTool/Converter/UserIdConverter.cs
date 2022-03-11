using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GarticWordsTool.Converter
{
    /// <summary>
    /// <see cref="User.id">用户ID</see>转换器，处理未登录时值为数字无法直接反序列化的情况
    /// </summary>
    public class UserIdConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int value = -1;
            try
            {
                reader.TryGetInt32(out value);
            }
            catch
            {

            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
