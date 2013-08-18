using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Synthesis.Templates;
using Synthesis.FieldTypes;
using Synthesis.ContentSearch;

namespace Synthesis.Generation
{
    public abstract class GeneratorBase
    {
        public GeneratorBase() { }

        public abstract void Initialize(IGeneratorParametersProvider generatorParamtersProvider,
                                        ITemplateInputProvider templateInputProvider,
                                        ITemplateSignatureProvider templateSignatureProvider,
                                        IFieldMappingProvider fieldMappingProvider,
                                        ISynthesisIndexFieldNameTranslator indexFieldNameTranslator);

        public abstract void GenerateToDisk();
    }
}
