﻿using System;
using System.IO;
using System.Text;
using UConvertPlugin;

namespace UAssetConverterPlugins
{
    public class Int64Property : IConverterPlugin
    {
        public long Value;

        public string GetPropertyName() => "Int64Property";
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
                this.Value = br.ReadInt64();
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
