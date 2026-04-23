using System.Reflection;
using Microsoft.AspNetCore.Http;
using Moq;
using Sondor.Queries;

namespace Sondor.HttpClient.Tests;

[TestFixture]
[TestOf(typeof(SondorEnvelope<>))]
public class SondorEnvelopeTest
{
    [Test]
    [TestCase(new[] { "Item1", "Item2" }, 10, 1, 5, 2, true)]
    [TestCase(new[] { "Item1" }, 1, 1, 1, 1, false)]
    [TestCase(new string[] { }, 0, 1, 10, 0, false)]
    public void BuildEnvelope_ValidInputs_ReturnsCorrectEnvelope(
        string[] items, long totalItems, int page, int pageSize, int expectedTotalPages, bool expectedHasNext)
    {
        // Arrange
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(r => r.Path).Returns("/api/test");
        mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

        var mockQuery = new Mock<IEnvelopeQuery>();
        mockQuery.Setup(q => q.Page).Returns(page);
        mockQuery.Setup(q => q.PageSize).Returns(pageSize);
        mockQuery.Setup(q => q.Sorts).Returns(SortQuery.Empty);
        mockQuery.Setup(q => q.Filters).Returns(FilterQuery.Empty);
        mockQuery.Setup(q => q.Search).Returns(SearchQuery.Empty);
        mockQuery.Setup(q => q.Fields).Returns(FieldsQuery.All);

        // Act
        var envelope =
            SondorEnvelope<string>.BuildEnvelope(items, totalItems, mockHttpContextAccessor.Object, mockQuery.Object);

        // Assert
        Assert.AreEqual(items, envelope.Items);
        Assert.AreEqual(expectedTotalPages, envelope.Meta.TotalPages);
        Assert.AreEqual(page, envelope.Meta.Page);
        Assert.AreEqual(pageSize, envelope.Meta.PageSize);
        Assert.AreEqual(totalItems, envelope.Meta.TotalItems);
        Assert.AreEqual(expectedHasNext, envelope.Meta.HasNext);
        Assert.IsNotNull(envelope.Links.First);
        Assert.IsNotNull(envelope.Links.Last);
        Assert.IsNotNull(envelope.Links.Next);
        Assert.IsNotNull(envelope.Links.Previous);
        Assert.IsNotNull(envelope.Links.Self);
    }

    [Test]
    [TestCase(1, "/api/test?page=1&pageSize=5")]
    [TestCase(2, "/api/test?page=2&pageSize=5")]
    public void BuildLink_ValidInputs_ReturnsCorrectLink(int page, string expectedLink)
    {
        // Arrange
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(r => r.Path).Returns("/api/test");
        mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

        var mockQuery = new Mock<IEnvelopeQuery>();
        mockQuery.Setup(q => q.PageSize).Returns(5);
        mockQuery.Setup(q => q.Sorts).Returns(SortQuery.Empty);
        mockQuery.Setup(q => q.Filters).Returns(FilterQuery.Empty);
        mockQuery.Setup(q => q.Search).Returns(SearchQuery.Empty);
        mockQuery.Setup(q => q.Fields).Returns(FieldsQuery.All);

        // Act
        var link = typeof(SondorEnvelope<string>)
            .GetMethod("BuildLink", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            ?.Invoke(null, new object[] { page, mockHttpContextAccessor.Object, mockQuery.Object });

        // Assert
        Assert.AreEqual(expectedLink, link);
    }

    [Test]
    public void BuildLink_NullHttpContextAccessor_ThrowsArgumentNullException()
    {
        // Arrange
        var mockQuery = new Mock<IEnvelopeQuery>();

        // Act & Assert
        Assert.Throws<TargetInvocationException>(() =>
            typeof(SondorEnvelope<string>)
                .GetMethod("BuildLink",
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.Invoke(null, [1, null, mockQuery.Object]));
    }
}