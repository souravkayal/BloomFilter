using System.Security.Cryptography;

namespace Bloom.HashPlugin
{
    internal class HSHA1 : IHash
    {
        public byte[] ComputeHash(byte[] data)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(data);
            }
        }
    }
}
