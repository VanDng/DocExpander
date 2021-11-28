using CodeSnipping.Exporters.Image;
using CodeSnipping.Loader;
using System;
using Xunit;

namespace TestProject1
{
    public class TestImageExporter
    {
        [Fact]
        public void Test1()
        {
            var loader = new CodeLoader();

            loader.Option = new LoadOption()
            {
                ContextLineCount = 5
            };

            var codeContent = loader.Load(@"TestData\emitarm64.cpp", 30);

            codeContent.SaveAsImage("tmp.html");
        }
    }
}
