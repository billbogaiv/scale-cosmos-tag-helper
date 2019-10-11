using Translator;

namespace ScaleCosmosTagHelper
{
    internal class ScaleCosmosContext
    {
        public Measurement2d Measurements { get; set; }
        public Translator<string, decimal> Translator { get; set; }
    }
}
