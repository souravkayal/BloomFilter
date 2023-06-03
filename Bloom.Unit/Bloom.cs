using NUnit.Framework;

[TestFixture]
public class BloomFilterTests
{
    private BloomFilter bloomFilter;

    [SetUp]
    public void SetUp()
    {
        // Set up a Bloom filter with an expected item count of 100 and a false positive rate of 0.01
        bloomFilter = new BloomFilter(100, 0.01);
    }

    [Test]
    public void Add_ItemExistsInFilter_ReturnsTrueForContains()
    {
        // Arrange
        string item = "item1";

        // Act
        bloomFilter.Add(item);

        // Assert
        Assert.IsTrue(bloomFilter.Contains(item));
    }

    [Test]
    public void Add_ItemDoesNotExistInFilter_ReturnsFalseForContains()
    {
        // Arrange
        string item1 = "item1";
        string item2 = "item2";

        // Act
        bloomFilter.Add(item1);

        // Assert
        Assert.IsFalse(bloomFilter.Contains(item2));
    }

    [Test]
    public void Contains_EmptyFilter_ReturnsFalse()
    {
        // Arrange
        string item = "item1";

        // Act & Assert
        Assert.IsFalse(bloomFilter.Contains(item));
    }

    [Test]
    public void Contains_ItemAddedToFilter_ReturnsTrue()
    {
        // Arrange
        string item = "item1";
        bloomFilter.Add(item);

        // Act & Assert
        Assert.IsTrue(bloomFilter.Contains(item));
    }

    [Test]
    public void Contains_ItemNotAddedToFilter_ReturnsFalse()
    {
        // Arrange
        string item1 = "item1";
        string item2 = "item2";
        bloomFilter.Add(item1);

        // Act & Assert
        Assert.IsFalse(bloomFilter.Contains(item2));
    }

    [Test]
    public void Contains_MultipleItemsAdded_ReturnsTrueForAddedItems()
    {
        // Arrange
        string item1 = "item1";
        string item2 = "item2";
        string item3 = "item3";

        bloomFilter.Add(item1);
        bloomFilter.Add(item2);
        bloomFilter.Add(item3);

        // Act & Assert
        Assert.IsTrue(bloomFilter.Contains(item1));
        Assert.IsTrue(bloomFilter.Contains(item2));
        Assert.IsTrue(bloomFilter.Contains(item3));
    }
}