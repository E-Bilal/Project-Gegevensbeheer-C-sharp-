using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.PBKDF
{
    public static class Salt
    {
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length / 2)
                             .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
                             .ToArray();
        }
        public static byte[] salt = HexStringToByteArray("f2c9e2ef3d7b440faf63613f853701cc7aa205f7");
    }
}
