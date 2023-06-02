
using System.Collections;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Implementation of bloom filter 
/// </summary>
public class BloomFilter
{
    private BitArray bitArray;
    private int size;
    private int numHashFunctions;

    /// <summary>
    /// Default constructor with static value
    /// </summary>
    public BloomFilter()
    {
        this.size = 1000;
        this.bitArray = new BitArray(size);
        this.numHashFunctions = 5;
    }

    /// <summary>
    /// Constructor to take size of filter and number of hashing to be applied
    /// </summary>
    /// <param name="size">Size of the bloom filter</param>
    /// <param name="hashFunctionNumber">Number of hashing times</param>
    public BloomFilter(int size, int hashFunctionNumber)
    {
        this.size = size;
        this.bitArray = new BitArray(size);
        this.numHashFunctions = hashFunctionNumber;
    }

    /// <summary>
    /// Add items to bloom filter
    /// </summary>
    /// <param name="item">Item to be pushed in bloom filter</param>
    public void Add(string item)
    {
        byte[] itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
        for (int i = 0; i < numHashFunctions; i++)
        {
            int hash = ComputeHash(itemBytes, i);
            bitArray[hash] = true;
        }
    }

    /// <summary>
    /// Check whether the item exist in filter
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(string item)
    {
        byte[] itemBytes = Encoding.UTF8.GetBytes(item);
        for (int i = 0; i < numHashFunctions; i++)
        {
            int hash = ComputeHash(itemBytes, i);
            if (!bitArray[hash])
                return false;
        }
        return true;
    }

    /// <summary>
    /// Compute hash of the item which need to check against bloom filter
    /// </summary>
    /// <param name="itemBytes"></param>
    /// <param name="hashIndex"></param>
    /// <returns></returns>
    private int ComputeHash(byte[] itemBytes, int hashIndex)
    {
        using (var sha1 = SHA1.Create())
        {
            byte[] hashBytes = sha1.ComputeHash(itemBytes);
            int hash = BitConverter.ToInt32(hashBytes, 0);
            return Math.Abs(hash + hashIndex) % size;
        }
    }

}


public class Program
{
    public static void Main(String[]args)
    {

        int filterSize = 1000;
        int hashTines = 5;

        BloomFilter filter = new BloomFilter(filterSize, hashTines);

        // Adding items to the filter
        filter.Add("apple");
        filter.Add("banana");
        filter.Add("orange");

        // Checking membership
        Console.WriteLine(filter.Contains("apple"));    // true
        Console.WriteLine(filter.Contains("grape"));    // false
        Console.WriteLine(filter.Contains("banana"));   // true

        Console.ReadLine();
    }
    
}