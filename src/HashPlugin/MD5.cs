using System.Security.Cryptography;

namespace Bloom.HashPlugin
{
    internal class HMD5 : IHash
    {
        public byte[] ComputeHash(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }
    }
}
