using Moq;
using System;
using Xunit;

namespace JumpListReader.Tests
{
    public class JumpListReaderTests
    {
        const string EMPTY_STRING = "";
        
        private readonly JumpListReader JumpListReader;

        public JumpListReaderTests()
        {
            JumpListReader = new JumpListReader { AumidList = new AumidList() };
        }

        [Theory]
        [InlineData(EMPTY_STRING)]
        [InlineData(null)]
        public void ReadForExe_ShouldThrowArgumentException_WhenNullOrEmpty(string path)
        {
            Assert.Throws<ArgumentException>(() => JumpListReader.ReadForExe(path));
        }

        [Fact]
        public void ReadForExe_ShouldReturnNull_WhenAumidNull()
        {
            var path = "test";

            var aumidList = Mock.Of<IAumidList>();
            Mock.Get(aumidList)
                .Setup(x => x.GetAumid(path))
                .Returns((string)null);

            JumpListReader.AumidList = aumidList;

            var result = JumpListReader.ReadForExe(path);

            Assert.Null(result);
        }
    }
}
