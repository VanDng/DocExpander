using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSnipping.Loader
{
    public class CodeLine
    {
        public string PlainText { get; private set; }
        public int LineNumber { get; private set; }

        public CodeLine(string plainText, int lineNumer)
        {
            PlainText = plainText;
            LineNumber = lineNumer;
        }
    }
}
