using System;
using System.IO;
using System.Text;
using UConvertPlugin;

namespace UAssetConverterPlugins
{
    public class VectorProperty : IConverterPlugin
    {
        public float X;
        public float Y;
        public float Z;

        public string GetPropertyName() => "Vector";
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
                this.X = br.ReadSingle();
                this.Y = br.ReadSingle();
                this.Z = br.ReadSingle();
            }
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            byte[] bytes = new byte[12];
            Array.Copy(BitConverter.GetBytes(this.X), 0, bytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(this.Y), 0, bytes, 4, 4);
            Array.Copy(BitConverter.GetBytes(this.Z), 0, bytes, 8, 4);
            return bytes;
        }
    }
}
