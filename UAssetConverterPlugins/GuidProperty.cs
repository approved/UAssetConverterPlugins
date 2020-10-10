using System;
using System.Collections.Generic;
using System.Text;
using UConvertPlugin;
using UConvertPlugin.Unreal;

namespace UAssetConverterPlugins
{
    class GuidProperty : IConverterPlugin
    {
        public FGuid Value;

        public string GetPropertyName() => "Guid";
        public bool HasTagData() => false;
        public bool HasTagValue() => true;

        public void DeserializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public void DeserializePropertyTagValue(IAssetConverter converter)
        {
            this.Value = new FGuid(converter.GetExportStream());
        }

        public byte[] SerializePropertyTagData(IAssetConverter converter)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializePropertyTagValue(IAssetConverter converter)
        {
            //TODO: Implement FGuid reading
            throw new NotImplementedException();
        }
    }
}
