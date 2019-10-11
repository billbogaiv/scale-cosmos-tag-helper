using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;
using Translator;

namespace ScaleCosmosTagHelper
{
    [HtmlTargetElement("*", Attributes = "sc-container")]
    public class ScaleCosmosContainerTagHelper : ScaleCosmos
    {
        public string Height { get; set; }
        public Translator<string, decimal> Translator { get; set; } = DefaultTranslator.SingleInstance;
        public string Width { get; set; }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Measurement2d.TryParse(Height, Width, out var measurement))
            {
                context.Items.Add(nameof(ScaleCosmosContext), new ScaleCosmosContext()
                {
                    Measurements = measurement,
                    Translator = Translator ?? DefaultTranslator.SingleInstance
                });

                Translator.TryTranslation(measurement.Height.Value, measurement.Height.Unit, measurement.Width.Unit, out var translatedHeight);

                var childContent = await output.GetChildContentAsync();

                output.Content.Clear();

                output.Content.AppendHtml(
                    $"<div data-sc-fluid-container style=\"height:calc(100% * {translatedHeight / measurement.Width.Value});width:calc(100% * {measurement.Width.Value / translatedHeight});max-height:100%;max-width:100%;\">");

                output.Content.AppendHtml(childContent);

                output.Content.AppendHtml("</div>");

                output.Attributes.SetAttribute(
                    "style",
                    $"--sc-ratio-height:{translatedHeight / measurement.Width.Value};--sc-ratio-width:1");
            }
            else
            {
                throw new Exception(
                    "Could not determine measurements. Make sure to have a space between the value and unit.");
            }

            if (output.Attributes.TryGetAttribute("sc-container", out var attribute))
            {
                output.Attributes.Remove(attribute);
            }

            output.Attributes.Add("data-sc-fixed-container", null);
        }
    }
}