# BloomFilter

**What is bloom filter ?
Bollm filter is space efficient probablistic data structure which is invented in 1970 by Burton Howard Bloom. It comes with risk of false positive result but not false negative. 
I.e It can indicate that the value is present where it is not actually present but it never otherwise. 

# Real systems which use bloom filter ?

In distributed system :

Distributed system like cassendra uses bloom filter to avoid checking data on SSTable for a partition being requested. The detail can be read here - https://cassandra.apache.org/doc/latest/cassandra/operating/bloom_filters.html

Hbase uses bloom filter to check whether a line or data information is present inside a file or not - https://hbase.apache.org/2.2/devapidocs/org/apache/hadoop/hbase/util/BloomFilter.html

In Cache :
Redis use bloom filter and it's capability to implemented probablistic data structure called RedisBloom. - https://redis.io/docs/stack/bloom/

In Spell check :
A full compiled bloom filter can be used to check existency of word. 

# Implementation using c# .NET 
It's very simple form of bloom filter using c#. 
