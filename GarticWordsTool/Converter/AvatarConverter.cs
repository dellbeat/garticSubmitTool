using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GarticWordsTool.Converter
{
    /// <summary>
    /// <see cref="User.avatar">头像地址</see>转换器，处理未登录时值为数字无法直接反序列化的情况
    /// </summary>
    public class AvatarConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = default(string);
            try
            {
                value = reader.GetString();
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
