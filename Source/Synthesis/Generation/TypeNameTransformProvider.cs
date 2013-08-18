using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Synthesis.Generation
{
    public class TypeNameTransformProvider : ITypeNameTransformProvider
    {
        private class Transform
        {
            public Transform(string pattern, string replaceWith)
            {
                Regex = new Regex(pattern);
                ReplaceWith = replaceWith;
            }

            public Regex Regex { get; private set; }
            public string ReplaceWith { get; private set; }
        }

        private List<Transform> _Transforms = new List<Transform>();

        public void AddTransform(XmlNode node)
        {
            _Transforms.Add(new Transform(node.Attributes["pattern"].Value, node.Attributes["replaceWith"].Value));
        }

        public string ApplyTransforms(string typeName)
        {
            // Output string.
            string output = typeName;

            // Loop through the transforms and apply each one.
            foreach (var transform in _Transforms)
                output = transform.Regex.Replace(output, transform.ReplaceWith);

            // Return the output string.
            return output;
        }
    }
}
