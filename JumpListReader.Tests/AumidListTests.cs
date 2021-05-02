using System;
using Xunit;

namespace JumpListReader.Tests
{
    public class AumidListTests
    {
        const string EMPTY_STRING = "";

        private readonly AumidList aumidList;

        public AumidListTests()
        {
            aumidList = new AumidList();
        }

        [Theory]
        [InlineData(EMPTY_STRING)]
        [InlineData(null)]
        public void Calculate_ShouldThrowArgumentException_WhenNullOrEmpty(string path)
        {
            Assert.Throws<ArgumentException>(() => aumidList.GetAumid(path));
        }
    }
}
