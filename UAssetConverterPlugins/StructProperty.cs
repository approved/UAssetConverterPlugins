using System;
using System.Collections.Generic;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    public class StructProperty : IConverterPlugin
    {
        public FName StructType;
        public FGuid Guid;
        public List<object> Values;

        public string GetPropertyName() => "StructProperty";

        public bool HasTagData() => true;

        public bool HasTagValue() => true;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            this.StructType = new FName(converter.GetNameMap(), converter.GetExportStream());
            this.Guid = new FGuid(converter.GetExportStream());
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            this.Values = converter.GetValueProperties(this.StructType.Name);
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            byte[] bytes = new byte[24];
            int i = 0;
            foreach (FNameEntrySerialized name in converter.GetNameMap())
            {
                if (name.Name.Equals(this.StructType))
                {
                    Array.Copy(BitConverter.GetBytes(i), bytes, 4);
                    break;
                }
                i++;
            }
            Array.Copy(BitConverter.GetBytes(this.StructType.NameCount), 0, bytes, 4, 4);
            Array.Copy(BitConverter.GetBytes(this.Guid.A), 0, bytes, 8, 4);
            Array.Copy(BitConverter.GetBytes(this.Guid.B), 0, bytes, 12, 4);
            Array.Copy(BitConverter.GetBytes(this.Guid.C), 0, bytes, 16, 4);
            Array.Copy(BitConverter.GetBytes(this.Guid.D), 0, bytes, 20, 4);
            return bytes;
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }
    }
}
