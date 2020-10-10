using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UConvertPlugin;

namespace UAssetConverterPlugins
{
    public class UInt16Property : IConverterPlugin
    {
        public ushort Value;

        public string GetPropertyName() => "UInt16Property";
        public bool HasTagData() => false;
        public bool HasTagValue() => true;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            using (BinaryReader br = new BinaryReader(converter.GetExportStream(), Encoding.UTF8, true))
            {
                this.Value = br.ReadUInt16();
            }
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            return BitConverter.GetBytes(this.Value);
        }
    }
}
