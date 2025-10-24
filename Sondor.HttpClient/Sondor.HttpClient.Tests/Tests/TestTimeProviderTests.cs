namespace Sondor.HttpClient.Tests.Tests;

/// <summary>
/// Tests for <see cref="TestTimeProvider"/>.
/// </summary>
[TestFixture]
public class TestTimeProviderTests
{
    /// <summary>
    /// Ensures that <see cref="TestTimeProvider.GetUtcNow"/> returns the expected value.
    /// </summary>
    [Test]
    public void GetUtcNow()
    {
        // arrange
        var timeProvider = new TestTimeProvider();
        var expected = new DateTimeOffset(DateTimeOffset.UtcNow.Year,
            DateTimeOffset.UtcNow.Month,
            DateTimeOffset.UtcNow.Day,
            DateTimeOffset.UtcNow.Hour,
            0,
            0,
            DateTimeOffset.UtcNow.Offset);

        // act
        var actual = timeProvider.GetUtcNow();
        
        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}