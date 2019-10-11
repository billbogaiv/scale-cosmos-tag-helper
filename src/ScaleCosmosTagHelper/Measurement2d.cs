using System.Globalization;
using System.Linq;

namespace ScaleCosmosTagHelper
{
    internal class Measurement2d
    {
        public Measurement Height { get; set; }
        public Measurement Width { get; set; }

        public static bool TryParse(
            string height,
            string width,
            out Measurement2d result)
        {
            decimal heightValue = default;
            decimal widthValue = default;

            if (
                !decimal.TryParse(height.Split(' ').FirstOrDefault(), out heightValue) ||
                !decimal.TryParse(width.Split(' ').FirstOrDefault(), out widthValue))
            {
                result = null;

                return false;
            }

            var heightUnit = height
                .Replace(heightValue.ToString(), string.Empty)
                .Trim();
            var widthUnit = width
                .Replace(widthValue.ToString(), string.Empty)
                .Trim();

            if (string.IsNullOrEmpty(heightUnit) || string.IsNullOrEmpty(widthUnit))
            {
                result = null;

                return false;
            }

            result = new Measurement2d()
            {
                Height = new Measurement()
                {
                    Unit = heightUnit,
                    Value = heightValue
                },
                Width = new Measurement()
                {
                    Unit = widthUnit,
                    Value = widthValue
                }
            };

            return true;
        }
    }
}
