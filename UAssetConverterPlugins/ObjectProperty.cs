using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    public class ObjectProperty : IConverterPlugin
    {
        public FName Value;

        public string GetPropertyName() => "ObjectProperty";
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
                int tableIndex = br.ReadInt32();
                if (tableIndex < 0)
                {
                    FObjectImport import = converter.GetImportMap()[tableIndex * -1 - 1];
                    this.Value = new FName($"{import.ObjectName.Name} : {import.ClassPackage.Name}/{import.ClassName.Name}")
                    {
                        NameCount = import.ObjectName.NameCount
                    };
                }
                else if (tableIndex > 0)
                {
                    this.Value = converter.GetExportMap()[tableIndex - 1].ObjectName;
                }
                else
                {
                    this.Value = new FName("UObject");
                }
            }
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            byte[] bytes = new byte[8];
            int i = 0;
            foreach (FNameEntrySerialized name in converter.GetNameMap())
            {
                if (name.Name.Equals(this.Value))
                {
                    Array.Copy(BitConverter.GetBytes(i), bytes, 4);
                    break;
                }
                i++;
            }
            Array.Copy(BitConverter.GetBytes(this.Value.NameCount), 0, bytes, 4, 4);
            return bytes;
        }
    }
}
