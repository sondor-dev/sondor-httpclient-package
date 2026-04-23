namespace Sondor.HttpClient.Tests;

/// <summary>
/// Tests for <see cref="SondorEnvelopeLinks"/>.
/// </summary>
[TestFixture]
public class SondorEnvelopeLinksTests
{
    /// <summary>
    /// Verifies that the <see cref="SondorEnvelopeLinks"/> constructor correctly initializes
    /// all properties with the provided values.
    /// </summary>
    /// <remarks>
    /// This test ensures that the <see cref="SondorEnvelopeLinks"/> instance is created with
    /// the expected values for the <c>First</c>, <c>Last</c>, <c>Next</c>, <c>Previous</c>, and <c>Self</c> properties.
    /// </remarks>
    [Test]
    public void Constructor()
    {
        // arrange
        const string first = "first";
        const string last = "last";
        const string next = "next";
        const string previous = "previous";
        const string self = "self";
        
        // act
        var envelopeLinks = new SondorEnvelopeLinks(first,
            last,
            next,
            previous,
            self);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(envelopeLinks.First, Is.EqualTo(first));
            Assert.That(envelopeLinks.Last, Is.EqualTo(last));
            Assert.That(envelopeLinks.Next, Is.EqualTo(next));
            Assert.That(envelopeLinks.Previous, Is.EqualTo(previous));
            Assert.That(envelopeLinks.Self, Is.EqualTo(self));
        }
    }
}