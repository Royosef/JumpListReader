using System;

namespace JumpListReader
{
    /*
     * The AppIdCalculator class based on the "appid_calc.pl" perl script by Adam from hexacorn:
     * https://www.hexacorn.com/blog/2013/04/30/jumplists-file-names-and-appid-calculator/
    */

    public class AppIdCalculator
    {
        public static string Calculate(string appUserModelId)
        {
            if (string.IsNullOrWhiteSpace(appUserModelId)) throw new ArgumentException($"{nameof(appUserModelId)} could not be null or whitespace.");

            var normalizedID = $"{string.Join("\0", appUserModelId.ToUpper().ToCharArray())}\0";

            return crc64(normalizedID);
        }

        private static string crc64(string data)
        {
            var crc = 0xFFFFFFFFFFFFFFFF;
            var crcTable = GetCrc64Table();

            while (data.Length > 0)
            {
                var c = (ulong)data[0];
                crc = (crc >> 8) ^ crcTable[(crc ^ c) & 0xFF];
                data = data.Substring(1);
            }

            return string.Format("{0:X8}{1:X8}", crc >> 32, crc & 0xFFFFFFFF).ToLower();
        }

        private static ulong[] GetCrc64Table()
        {
            const ulong POLY_64 = 0x92C64265D32139A4;

            var crcTable = new ulong[256];

            for (ulong i = 0; i < 256; i++)
            {
                var lv = i;

                for (var j = 0; j < 8; j++)
                {
                    var fl = lv & 1;
                    lv = lv >> 1;

                    if (fl == 1) lv = lv ^ POLY_64;

                    crcTable[i] = lv;
                }
            }

            return crcTable;
        }
    }
}
