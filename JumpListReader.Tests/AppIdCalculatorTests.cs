using System;
using Xunit;

namespace JumpListReader.Tests
{
    public class AppIdCalculatorTests
    {
        const string EMPTY_STRING = "";

        [Theory]
        [InlineData(EMPTY_STRING)]
        [InlineData(null)]
        public void Calculate_ShouldThrowArgumentException_WhenNullOrEmpty(string aumid)
        {
            Assert.Throws<ArgumentException>(() => AppIdCalculator.Calculate(aumid));
        }

        [Theory]
        [InlineData("Icons8LLC.Lunacy_7g68zyg4rddyp!App", "fb0d7b2d2a3bcce3")]
        [InlineData("Microsoft.MicrosoftEdge_8wekyb3d8bbwe!MicrosoftEdge", "9d1f905ce5044aee")]
        [InlineData("Microsoft.Office.WINWORD.EXE.15", "fb3b0dbfee58fac8")]
        [InlineData("Microsoft.Office.EXCEL.EXE.15", "b8ab77100df80ab2")]
        [InlineData(@"D:\Program Files (x86)\WinSCP\WinSCP.exe", "ae4ec052916fb59b")]
        [InlineData(@"C:\Program Files (x86)\Notepad++\notepad++.exe", "a2b1a1e4176a75c8")]
        public void Calculate_ShouldReturnCorrectAppId_WhenValidAumid(string aumid, string appId)
        {
            var result = AppIdCalculator.Calculate(aumid);

            Assert.Equal(appId, result);
        }
    }
}
