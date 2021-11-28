using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSnipping.Loader
{
    public class CodeContent
    {
        public string FilePath { get; private set; }

        public CodeLine[] Lines { get; private set; }
        public CodeLanguage Language { get; private set; }

        public CodeContent(IEnumerable<CodeLine> lines, string filePath, CodeLanguage language)
        {
            FilePath = filePath;
            Lines = lines.ToArray();
            Language = language;
        }
    }
}
