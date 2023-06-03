
using Bloom.HashPlugin;
using System.Collections;
using System.Text;

/// <summary>
/// Implementation of bloom filter 
/// </summary>
public class BloomFilter
{
    private BitArray _filter;
    private int _numHashFunctions;
    private int _filterSize;
    private IHash _hash;

    /// <summary>
    /// Constructor to take size of filter and number of hashing to be applied
    /// </summary>
    /// <param name="expectedItemsCount"> Expected size of the bloom filter. </param>
    /// <param name="falsePositiveRate">Tollerance of false positive rate. </param>
    public BloomFilter(int expectedItemsCount, double falsePositiveRate)
    {
        var optimalValues = GetOptimalFilterSizeAndHashes(expectedItemsCount, falsePositiveRate);
        _filterSize = optimalValues.Item1;
        _numHashFunctions = optimalValues.Item2;
        _hash = new Bloom.HashPlugin.HMD5();
        _filter = new BitArray(_filterSize);
    }

    /// <summary>
    /// Constructor to take size of filter and number of hashing to be applied
    /// </summary>
    /// <param name="expectedItemsCount"> Expected size of the bloom filter. </param>
    /// <param name="falsePositiveRate">Tollerance of false positive rate. </param>
    public BloomFilter(int expectedItemsCount, double falsePositiveRate , IHash hash)
    {
        var optimalValues = GetOptimalFilterSizeAndHashes(expectedItemsCount, falsePositiveRate);
        _filterSize = optimalValues.Item1;
        _numHashFunctions = optimalValues.Item2;
        _hash = hash;
        _filter = new BitArray(_filterSize);
    }


    /// <summary>
    /// Add items to bloom filter
    /// </summary>
    /// <param name="item">Item to be set in bloom filter </param>
    public void Add(string item)
    {
        byte[] itemBytes = Encoding.UTF8.GetBytes(item);

        for (int i = 0; i < _numHashFunctions; i++)
        {
            int hash = GetHash(itemBytes, i);
            _filter[hash] = true;
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

        for (int i = 0; i < _numHashFunctions; i++)
        {
            int hash = GetHash(itemBytes, i);
            if (!_filter[hash])
                return false;
        }
        return true;
    }

    /// <summary>
    /// Compute hash of the item which need to check against bloom filter
    /// </summary>
    /// <param name="itemBytes">item which need to be hashed</param>
    /// <param name="hashIndex">Index of hash time's </param>
    /// <returns></returns>
    private int GetHash(byte[] itemBytes, int hashIndex)
    {
        return Math.Abs((BitConverter.ToInt32(_hash.ComputeHash(itemBytes), 0) ^ hashIndex)  % _filterSize);
    }

    /// <summary>
    /// Function to generate optimal filter size and times of hashing by expected item count and expected false positive rate 
    /// </summary>
    /// <param name="expectedItemsCount">Number of items are expected to be keep in bloom filter </param>
    /// <param name="falsePositiveRate">Rate of false positive </param>
    /// <returns></returns>
    private (int, int) GetOptimalFilterSizeAndHashes(int expectedItemsCount, double falsePositiveRate)
    {
        int filterSize = (int)Math.Ceiling(-(expectedItemsCount * Math.Log(falsePositiveRate)) / Math.Pow(Math.Log(2), 2));
        int numHashFunctions = (int)Math.Ceiling((filterSize / expectedItemsCount) * Math.Log(2));

        return (filterSize, numHashFunctions );
    }
}

public class Program
{
    public static void Main(String[]args)
    {
        // Create a Bloom filter with an expected item count of 1000 and false positive rate of 0.01
        BloomFilter bloomFilter = new BloomFilter(1000, 0.01);

        // Add an item to the filter
        bloomFilter.Add("item1"); 
        bloomFilter.Add("item2");

        // Check if an item exists in the filter
        bool item1Exists = bloomFilter.Contains("item1"); 
        bool item3Exists = bloomFilter.Contains("item3");
        
        Console.WriteLine("Item 1 exists: " + item1Exists);
        Console.WriteLine("Item 3 exists: " + item3Exists);

        BloomFilter bloomFilterWithSHA = new BloomFilter(1000, 0.01, new HSHA1());

        Console.ReadLine();
    }
    
}