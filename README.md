<br/>
![image](https://github.com/souravkayal/BloomFilter/assets/6651731/40c3521b-9556-4d06-9339-dcf2fdc36151)

<br/>

# BloomFilter

Bollm filter is space efficient probablistic data structure which is invented in 1970 by Burton Howard Bloom. It comes with risk of false positive result but not false negative. 
I.e It can indicate that the value is present where it is not actually present but it never otherwise. 

# A Few real systems which use bloom filter.

<b> In distributed system : </b>

Distributed system like Cassandra uses bloom filter to avoid checking data on SSTable for a partition being requested. The detail can be read here - https://cassandra.apache.org/doc/latest/cassandra/operating/bloom_filters.html

Hbase uses bloom filter to check whether a line or data information is present inside a file or not - https://hbase.apache.org/2.2/devapidocs/org/apache/hadoop/hbase/util/BloomFilter.html

<b> In Cache: </b> </br></br>
Redis use bloom filter and it's capability to implemented probabilistic data structure called RedisBloom. - https://redis.io/docs/stack/bloom/

<b> In Spell check: </b> <br/></br>
A full compiled bloom filter can be used to check existence of word. 

# Implementation using c# .NET 

<b> Adding value to bloom filter: </b> <br><br>
Add() method is implemented to insert value in bloom filter. The algorithm will generate hash of input value by using SHA1 hashing algorithm and set specific bit in filter.  
</br>
Time Complexciy : O(n) </br>
Space Complexcity O(1)

```cs
void Add(string item)
```

<b> Check existence of value in filter: </b> <br><br>
Need to call Contains (string item) method to check existence of value against with filter. It will return true if there is chance to presence otherwise false.

```cs
bool Contains(string item)
```

Time Complexciy : O(n) </br>
Space Complexcity O(1)

<b> Add and check presence of value in bloom filter </b> <br> <br>

```cs

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

        Console.ReadLine();
    }
```  
<br> <b> Inject hashing algoritam while creating object of bloom filter </b>
 
```cs
  BloomFilter bloomFilterWithSHA = new BloomFilter(1000, 0.01, new HSHA1());
```
 
 
 
