namespace Bloom.HashPlugin
{
    public interface IHash
    {
        byte[] ComputeHash(byte[] data);
    }
}
