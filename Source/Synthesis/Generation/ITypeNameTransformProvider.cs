using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Synthesis.Generation
{
    public interface ITypeNameTransformProvider
    {
        void AddTransform(XmlNode node);
        string ApplyTransforms(string typeName);
    }
}
