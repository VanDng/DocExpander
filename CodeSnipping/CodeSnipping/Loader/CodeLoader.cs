using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeSnipping.Loader
{
    public class CodeLoader
    {
        public LoadOption Option { get; set; }

        public CodeLoader()
        {
            Option = new LoadOption()
            {
                ContextLineCount = 10
            };
        }

        public CodeContent Load(string sourceCodeFilePath, int lineNumber)
        {
            var codeLines = ReadCodeLine(sourceCodeFilePath, lineNumber);
            var filePath = ReadCodeFilePath(sourceCodeFilePath);
            var language = ReadCodeLanguage(sourceCodeFilePath);

            return new CodeContent(codeLines, filePath, language);
        }

        private CodeLanguage ReadCodeLanguage(string codeFilePath)
        {
            var extension = Path.GetExtension(codeFilePath);

            var language = CodeLanguage.Unknown;

            switch (extension)
            {
                case ".cs":
                    language = CodeLanguage.CSharp;
                    break;

                case ".c":
                    language = CodeLanguage.C;
                    break;

                case ".cpp":
                    language = CodeLanguage.CPlusPlus;
                    break;

                default:
                    language = CodeLanguage.Unknown;
                    break;
            }

            return language;
        }

        private string ReadCodeFilePath(string codeFilePath)
        {
            return codeFilePath;
        }

        private IEnumerable<CodeLine> ReadCodeLine(string codeFilePath, int lineNumber)
        {
            var rawLines = File.ReadLines(codeFilePath);

            var codeLines = new List<CodeLine>();

            codeLines.Add(
                new CodeLine(rawLines.ElementAt(lineNumber), lineNumber)
            );

            if (Option.ContextLineCount > 0)
            {
                ReadContext(rawLines, lineNumber, codeLines);
            }

            return codeLines;
        }

        private void ReadContext(IEnumerable<string> rawLines, int baseLineNumber, List<CodeLine> codeLines)
        {
            // Up
            for (int idx = baseLineNumber - 1;
                idx >= 0 && idx >= baseLineNumber - Option.ContextLineCount;
                idx--)
            {
                codeLines.Insert(0,
                    new CodeLine(rawLines.ElementAt(idx), idx)
                );
            }

            // Down
            for (int idx = baseLineNumber + 1;
                idx <= rawLines.Count() - 1 && idx <= baseLineNumber + Option.ContextLineCount;
                idx++)
            {
                codeLines.Add(
                    new CodeLine(rawLines.ElementAt(idx), idx)
                );
            }
        }
    }
}
