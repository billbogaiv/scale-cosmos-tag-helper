using Translator;

namespace ScaleCosmosTagHelper
{
    public class DefaultTranslator : Translator<string, decimal>
    {
        public DefaultTranslator()
        {
            AddRange(new[]
            {
                new Translation<string, decimal>("ft", x => x , "ft", x => x ),
                new Translation<string, decimal>(12, "in", (x, seed) => x * seed, "ft", (x, seed) => x / seed),

                new Translation<string, decimal>("mi", x => x , "mi", x => x ),
                new Translation<string, decimal>(5280, "ft", (x, seed) => x * seed, "mi", (x, seed) => x / seed),
            });
        }

        public static readonly DefaultTranslator SingleInstance = new DefaultTranslator();
    }
}
