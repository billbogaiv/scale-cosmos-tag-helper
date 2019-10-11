using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace ScaleCosmosTagHelper
{
    [HtmlTargetElement("*", Attributes = "sc-object")]
    public class ScaleCosmosObjectTagHelper : ScaleCosmos
    {
        public string Height { get; set; }
        public string Width { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context.Items.TryGetValue(nameof(ScaleCosmosContext), out var tempContext) && tempContext is ScaleCosmosContext cosmosContext)
            {
                if (Measurement2d.TryParse(Height, Width, out var measurement))
                {
                    cosmosContext.Translator.TryTranslation(measurement.Height.Value, measurement.Height.Unit, cosmosContext.Measurements.Height.Unit, out var objectHeight);

                    cosmosContext.Translator.TryTranslation(measurement.Width.Value, measurement.Width.Unit, cosmosContext.Measurements.Width.Unit, out var objectWidth);

                    var scaleHeight = (objectHeight / cosmosContext.Measurements.Height.Value) * 100.0m;
                    var scaleWidth = (objectWidth / cosmosContext.Measurements.Width.Value) * 100.0m;

                    output.Attributes.SetAttribute("style", $"height:{scaleHeight}%;width:{scaleWidth}%;");
                }
                else
                {
                    throw new Exception(
                        "Could not determine measurements. Make sure to have a space between the value and unit.");
                }
            }
            else
            {
                throw new Exception(
                    "Could not retrieve parent-tag context.");
            }

            if (output.Attributes.TryGetAttribute("sc-object", out var attribute))
            {
                output.Attributes.Remove(attribute);
            }

            output.Attributes.Add("data-sc-object", null);

            return Task.FromResult(true);
        }
    }
}
