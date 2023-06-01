
using System.Collections;
using System.Security.Cryptography;

public class BloomFilter
{
    private BitArray bitArray;
    private int size;
    private int numHashFunctions;

    public BloomFilter(int size, int expectedElements)
    {
        this.size = size;
        this.bitArray = new BitArray(size);
        this.numHashFunctions = OptimalNumHashFunctions(size, expectedElements);
    }

    public void Add(string item)
    {
        byte[] itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
        for (int i = 0; i < numHashFunctions; i++)
        {
            int hash = ComputeHash(itemBytes, i);
            bitArray[hash] = true;
        }
    }

    public bool Contains(string item)
    {
        byte[] itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
        for (int i = 0; i < numHashFunctions; i++)
        {
            int hash = ComputeHash(itemBytes, i);
            if (!bitArray[hash])
                return false;
        }
        return true;
    }

    private int ComputeHash(byte[] itemBytes, int hashIndex)
    {
        using (var md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(itemBytes);
            int hash = BitConverter.ToInt32(hashBytes, 0);
            return Math.Abs(hash + hashIndex) % size;
        }
    }

    private int OptimalNumHashFunctions(int size, int expectedElements)
    {
        // Formula: k = (m / n) * ln(2)
        double numHashes = (size / (double)expectedElements) * Math.Log(2);
        return (int)Math.Ceiling(numHashes);
    }
}


public class Program
{
    public static void Main(String[]args)
    {

        int filterSize = 1000;
        int expectedElements = 100;

        BloomFilter filter = new BloomFilter(filterSize, expectedElements);

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