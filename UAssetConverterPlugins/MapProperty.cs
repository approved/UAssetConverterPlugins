using System;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    public class MapProperty : IConverterPlugin
    {
        public FName KeyType;
        public FName ValueType;

        public string GetPropertyName() => "MapProperty";

        public bool HasTagData() => true;

        //TODO: Implement Tag Value
        public bool HasTagValue() => false;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            this.KeyType = new FName(converter.GetNameMap(), converter.GetExportStream());
            this.ValueType = new FName(converter.GetNameMap(), converter.GetExportStream());
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            byte[] bytes = new byte[16];
            int i = 0;
            foreach (FNameEntrySerialized name in converter.GetNameMap())
            {
                if (name.Name.Equals(this.KeyType))
                {
                    Array.Copy(BitConverter.GetBytes(i), bytes, 4);
                    break;
                }
                i++;
            }
            Array.Copy(BitConverter.GetBytes(this.KeyType.NameCount), 0, bytes, 4, 4);
            foreach (FNameEntrySerialized name in converter.GetNameMap())
            {
                if (name.Name.Equals(this.ValueType))
                {
                    Array.Copy(BitConverter.GetBytes(i), 0, bytes, 8, 4);
                    break;
                }
                i++;
            }
            Array.Copy(BitConverter.GetBytes(this.ValueType.NameCount), 0, bytes, 12, 4);
            return bytes;
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }
    }
}
