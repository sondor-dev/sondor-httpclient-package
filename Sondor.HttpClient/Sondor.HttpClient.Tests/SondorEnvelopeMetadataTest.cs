namespace Sondor.HttpClient.Tests;

/// <summary>
/// Contains unit tests for the <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> record.
/// </summary>
/// <remarks>
/// This test class verifies the correctness of the <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> implementation, 
/// including its constructors, equality members, and other functionality.
/// </remarks>
[TestFixture]
[TestOf(typeof(SondorEnvelopeMetadata))]
public class SondorEnvelopeMetadataTest
{
    /// <summary>
    /// Verifies that the constructor of the <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> record 
    /// correctly initializes its properties.
    /// </summary>
    /// <param name="totalPages">The total number of pages to initialize.</param>
    /// <param name="page">The current page number to initialize.</param>
    /// <param name="pageSize">The size of each page to initialize.</param>
    /// <param name="totalItems">The total number of items to initialize.</param>
    /// <param name="hasNext">Indicates whether there is a subsequent page to initialize.</param>
    /// <remarks>
    /// This test ensures that all properties of the <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> 
    /// record are properly set when the constructor is invoked with valid arguments.
    /// </remarks>
    [Test]
    [TestCase(0, 0, 0, 0L, false)]
    [TestCase(1, 1, 10, 10L, true)]
    [TestCase(100, 50, 20, 2000L, true)]
    [TestCase(1, 1, 1, 1L, false)]
    [TestCase(int.MaxValue, int.MaxValue, int.MaxValue, long.MaxValue, true)]
    [TestCase(int.MinValue, int.MinValue, int.MinValue, long.MinValue, false)]
    public void Constructor_ShouldInitializePropertiesCorrectly(
        int totalPages, int page, int pageSize, long totalItems, bool hasNext)
    {
        // Arrange & Act
        var metadata = new SondorEnvelopeMetadata(totalPages, page, pageSize, totalItems, hasNext);

        // Assert
        Assert.AreEqual(totalPages, metadata.TotalPages);
        Assert.AreEqual(page, metadata.Page);
        Assert.AreEqual(pageSize, metadata.PageSize);
        Assert.AreEqual(totalItems, metadata.TotalItems);
        Assert.AreEqual(hasNext, metadata.HasNext);
    }

    /// <summary>
    /// Verifies that the equality operator returns <c>true</c> for two identical 
    /// <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> records.
    /// </summary>
    /// <remarks>
    /// This test ensures that two instances of <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> 
    /// with identical property values are considered equal.
    /// </remarks>
    /// <seealso cref="Sondor.HttpClient.SondorEnvelopeMetadata"/>
    [Test]
    public void Equality_ShouldReturnTrueForIdenticalRecords()
    {
        // Arrange
        var metadata1 = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);
        var metadata2 = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);

        // Act & Assert
        Assert.AreEqual(metadata1, metadata2);
    }

    /// <summary>
    /// Verifies that the equality comparison between two different 
    /// <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> records returns <c>false</c>.
    /// </summary>
    /// <remarks>
    /// This test ensures that two instances of <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> 
    /// with differing property values are not considered equal.
    /// </remarks>
    /// <example>
    /// For example, if one record has a total of 10 pages and another has 5 pages, 
    /// this test confirms that they are not equal.
    /// </example>
    [Test]
    public void Equality_ShouldReturnFalseForDifferentRecords()
    {
        // Arrange
        var metadata1 = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);
        var metadata2 = new SondorEnvelopeMetadata(5, 2, 10, 100L, false);

        // Act & Assert
        Assert.AreNotEqual(metadata1, metadata2);
    }

    /// <summary>
    /// Verifies that the <see cref="SondorEnvelopeMetadata.ToString"/> method 
    /// returns a correctly formatted string representation of the record.
    /// </summary>
    /// <remarks>
    /// This test ensures that the string output includes all properties of 
    /// <see cref="SondorEnvelopeMetadata"/> in a readable and accurate format.
    /// </remarks>
    /// <example>
    /// Example output:
    /// <c>"TotalPages = 10, Page = 1, PageSize = 20, TotalItems = 200, HasNext = True"</c>
    /// </example>
    [Test]
    public void ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var metadata = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);

        // Act
        var result = metadata.ToString();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Contains("TotalPages = 10"));
        Assert.IsTrue(result.Contains("Page = 1"));
        Assert.IsTrue(result.Contains("PageSize = 20"));
        Assert.IsTrue(result.Contains("TotalItems = 200"));
        Assert.IsTrue(result.Contains("HasNext = True"));
    }

    /// <summary>
    /// Verifies that the <see cref="Sondor.HttpClient.SondorEnvelopeMetadata.GetHashCode"/> method
    /// returns the same hash code for two identical <see cref="Sondor.HttpClient.SondorEnvelopeMetadata"/> records.
    /// </summary>
    /// <remarks>
    /// This test ensures the consistency and correctness of the hash code implementation
    /// for identical records, which is crucial for scenarios involving collections or hashing.
    /// </remarks>
    /// <seealso cref="Sondor.HttpClient.SondorEnvelopeMetadata"/>
    [Test]
    public void GetHashCode_ShouldReturnSameHashCodeForIdenticalRecords()
    {
        // Arrange
        var metadata1 = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);
        var metadata2 = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);

        // Act & Assert
        Assert.AreEqual(metadata1.GetHashCode(), metadata2.GetHashCode());
    }

    /// <summary>
    /// Verifies that the <see cref="SondorEnvelopeMetadata.GetHashCode"/> method returns different hash codes
    /// for records with differing property values.
    /// </summary>
    /// <remarks>
    /// This test ensures the correctness of the hash code implementation by asserting that
    /// two instances of <see cref="SondorEnvelopeMetadata"/> with different property values
    /// produce distinct hash codes.
    /// </remarks>
    /// <example>
    /// For example, given two records with different values for properties such as <c>TotalPages</c>,
    /// <c>Page</c>, <c>PageSize</c>, <c>TotalItems</c>, or <c>HasNext</c>, their hash codes
    /// should not be equal.
    /// </example>
    [Test]
    public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentRecords()
    {
        // Arrange
        var metadata1 = new SondorEnvelopeMetadata(10, 1, 20, 200L, true);
        var metadata2 = new SondorEnvelopeMetadata(5, 2, 10, 100L, false);

        // Act & Assert
        Assert.AreNotEqual(metadata1.GetHashCode(), metadata2.GetHashCode());
    }
}