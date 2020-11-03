using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    public class ArrayProperty : IConverterPlugin
    {
        public FName ArrayType;
        public int Count;
        public List<object> Properties = new List<object>();

        public string GetPropertyName() => "ArrayProperty";

        public bool HasTagData() => true;

        public bool HasTagValue() => true;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            this.ArrayType = new FName(converter.GetNameMap(), converter.GetExportStream());
        }

        //TODO: When deserializing StructProperty arrays, invalid data may occur. Must fix, unsure how at this time
        /**
         * Comment Author: Nick Kennedy
         * Date: 12 October 2020
         **/
        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            using (BinaryReader br = new BinaryReader(converter.GetExportStream(), Encoding.UTF8, true))
            {
                this.Count = br.ReadInt32();
                for (int i = 0; i < this.Count; i++)
                {
                    Properties.Add(converter.GetValueProperties(this.ArrayType.Name));
                }
            }
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            byte[] bytes = new byte[8];
            int i = 0;
            foreach (FNameEntrySerialized name in converter.GetNameMap())
            {
                if (name.Name.Equals(this.ArrayType))
                {
                    Array.Copy(BitConverter.GetBytes(i), bytes, 4);
                    break;
                }
                i++;
            }
            Array.Copy(BitConverter.GetBytes(this.ArrayType.NameCount), 0, bytes, 4, 4);
            return bytes;
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }
    }
}
