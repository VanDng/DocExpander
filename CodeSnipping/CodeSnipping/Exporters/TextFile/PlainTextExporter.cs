using CodeSnipping.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSnipping.Exporters.PlainText
{
    public static class PlainTextExporter
    {
        public static string SaveAsPlainText(this CodeContent codeContent)
        {
            var codeLines = codeContent.Lines.Select(s => s.PlainText);

            var codeText = string.Join(Environment.NewLine, codeLines);

            return codeText;
        }
    }
}
