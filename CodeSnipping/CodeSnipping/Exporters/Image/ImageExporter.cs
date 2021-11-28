using CodeSnipping.Loader;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSnipping.Exporters.Image
{
    public static class ImageExporter
    {
        private static string TemplateRepetitiveElement = "repetitive-element";
        private static string TemplateCodeLine = "<codeline></codeline>";

        public static void SaveAsImage(this CodeContent codeContent, string outputFilePath)
        {
            string template = @"Exporters\Image\ImageTemplate\Template1.html";
            string templateHtml = File.ReadAllText(template);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(templateHtml);

            var repetitiveNode = htmlDocument.DocumentNode.SelectSingleNode($"//*[contains(@class,'{TemplateRepetitiveElement}')]");
            var containerNode = repetitiveNode.ParentNode;

            containerNode.RemoveChild(repetitiveNode);

            var codeLines = codeContent.Lines;
            foreach(var codeLine in codeLines)
            {
                var oriHtml = repetitiveNode.InnerHtml;
                var newHtml = oriHtml.Replace(TemplateCodeLine, codeLine.PlainText);

                var newCodeNode = repetitiveNode.CloneNode(deep: true);
                newCodeNode.InnerHtml = newHtml;
                containerNode.AppendChild(newCodeNode);
            }

            using (var sw = new StreamWriter(outputFilePath))
            {
                htmlDocument.Save(sw);
            }
        }
    }
}
