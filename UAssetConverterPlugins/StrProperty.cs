using System;
using System.Text;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    public class StrProperty : IConverterPlugin
    {
        public FString Str;

        public string GetPropertyName() => "StrProperty";
        public bool HasTagData() => false;
        public bool HasTagValue() => true;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            this.Str = new FString(converter.GetExportStream());
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            byte[] bytes = new byte[this.Str.Value.Length + 1];
            Array.Copy(Encoding.UTF8.GetBytes(this.Str.Value), bytes, this.Str.Value.Length);
            return bytes;
        }
    }
}
