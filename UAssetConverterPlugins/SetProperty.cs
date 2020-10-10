using System;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    public class SetProperty : IConverterPlugin
    {
        public FName SetType;

        public string GetPropertyName() => "SetProperty";

        public bool HasTagData() => true;

        //TODO: Implement Tag Value
        public bool HasTagValue() => false;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            this.SetType = new FName(converter.GetNameMap(), converter.GetExportStream());
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            byte[] bytes = new byte[8];
            int i = 0;
            foreach (FNameEntrySerialized name in converter.GetNameMap())
            {
                if (name.Name.Equals(this.SetType))
                {
                    Array.Copy(BitConverter.GetBytes(i), bytes, 4);
                    break;
                }
                i++;
            }
            Array.Copy(BitConverter.GetBytes(this.SetType.NameCount), 0, bytes, 4, 4);
            return bytes;
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }
    }
}
