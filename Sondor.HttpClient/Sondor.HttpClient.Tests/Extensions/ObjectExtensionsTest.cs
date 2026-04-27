using Sondor.HttpClient.Extensions;

namespace Sondor.HttpClient.Tests.Extensions;

[TestFixture]
[TestOf(typeof(ObjectExtensions))]
public class ObjectExtensionsTest
{
    [Test]
    [TestCase("", ExpectedResult = "9D4568C009D203AB10E33EA9953A0264")] // Empty string
    [TestCase(" ", ExpectedResult = "FCC3D7489D15EF49DBBF735234234CF7")] // Single space
    [TestCase("test", ExpectedResult = "303B5C8988601647873B4FFD247D83CB")] // Simple string
    [TestCase("1234567890", ExpectedResult = "CD33A01FD5047C4CD74BC91D98F0920B")] // Numeric string
    [TestCase("{\"key\":\"value\"}", ExpectedResult = "5E5A279F8BE2876C1E02FEB3468EFE1E")] // JSON string
    [TestCase("{\"key\":null}", ExpectedResult = "A4029D476AC08A995C6ADCEBB737971F")] // JSON with null value
    [TestCase("{\"key\":123}", ExpectedResult = "A126ED921E0FC1D6885287835E5C543B")] // JSON with numeric value
    [TestCase("{\"key\":\"\"}", ExpectedResult = "F0E12DD21105CD394C4A85BF9A54936A")] // JSON with empty string value
    [TestCase("{\"key\":\" \"}", ExpectedResult = "53F043C94222977AEFBAEED7CDA63EE9")] // JSON with space string value
    [TestCase("{\"key\":\"test\"}",
        ExpectedResult = "9ECB7A2CB910CA0A24B717B35C6CC359")] // JSON with simple string value
    [TestCase("{\"key\":\"1234567890\"}",
        ExpectedResult = "24C7772A11DF9263B7F0CF6988E416E8")] // JSON with numeric string value
    [TestCase("{\"key\":{\"nestedKey\":\"nestedValue\"}}",
        ExpectedResult = "C9BD4053EF7736C653976C6E3DF54E4C")] // Nested JSON
    [TestCase("{\"key\":{\"nestedKey\":null}}",
        ExpectedResult = "1819B550E7BB2199B685F56BA2AA4B9E")] // Nested JSON with null value
    [TestCase("{\"key\":{\"nestedKey\":123}}",
        ExpectedResult = "3AAB7A12DA84ABE5D3FCC72EB55C54EB")] // Nested JSON with numeric value
    [TestCase("{\"key\":{\"nestedKey\":\"\"}}",
        ExpectedResult = "CCBB04C5855DDE499E2B9A3C241F3A43")] // Nested JSON with empty string value
    [TestCase("{\"key\":{\"nestedKey\":\" \"}}",
        ExpectedResult = "379F9978E926081CE7E1D4868AAB5D0B")] // Nested JSON with space string value
    [TestCase("{\"key\":{\"nestedKey\":\"test\"}}",
        ExpectedResult = "4432FB324AAE457930D21ED4BD9687CF")] // Nested JSON with simple string value
    [TestCase("{\"key\":{\"nestedKey\":\"1234567890\"}}",
        ExpectedResult = "53F5F25A3E9FEAD39AA637D80FE44087")] // Nested JSON with numeric string value
    public string CreateHash_ShouldReturnExpectedHashOrThrowException(object input)
    {
        return input.CreateHash();
    }
}