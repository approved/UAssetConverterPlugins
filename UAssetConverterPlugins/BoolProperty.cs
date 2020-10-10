using System;
using System.IO;
using System.Text;
using UConvertPlugin;

namespace UAssetConverterPlugins
{
    public class BoolProperty : IConverterPlugin
    {
        public bool Value;

        public string GetPropertyName() => "BoolProperty";

        public bool HasTagData() => true;

        public bool HasTagValue() => false;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            using (BinaryReader br = new BinaryReader(converter.GetExportStream(), Encoding.UTF8, true))
            {
                this.Value = br.ReadByte() != 0;
            }
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            return new byte[] { (byte)(this.Value ? 0 : 1) };
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }
    }
}
