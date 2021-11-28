using CodeSnipping.Loader;
using System;
using Xunit;

namespace TestProject1
{
    public class TestCodeLoader
    {
        [Fact]
        public void Test1()
        {
            var loader = new CodeLoader();

            loader.Option = new LoadOption()
            {
                ContextLineCount = 10
            };

            var content = loader.Load(@"TestData\emitarm64.cpp", 6);

            Assert.Equal(17, content.Lines.Length);
            Assert.Equal(6, content.Lines[6].LineNumber);
            Assert.Equal("XX                             emitArm64.cpp                                 XX",
                         content.Lines[6].PlainText);

            Assert.Equal(12, content.Lines[12].LineNumber);
            Assert.Equal(@"#include ""jitpch.h""",
                         content.Lines[12].PlainText);
        }

        [Fact]
        public void Test2()
        {
            var loader = new CodeLoader();

            loader.Option = new LoadOption()
            {
                ContextLineCount = 5
            };

            var content = loader.Load(@"TestData\emitarm64.cpp", 30);

            Assert.Equal(11, content.Lines.Length);

            Assert.Equal(25, content.Lines[0].LineNumber);
            Assert.Equal(string.Empty,
                         content.Lines[0].PlainText);

            Assert.Equal(35, content.Lines[10].LineNumber);
            Assert.Equal("};",
                         content.Lines[10].PlainText);
        }
    }
}
