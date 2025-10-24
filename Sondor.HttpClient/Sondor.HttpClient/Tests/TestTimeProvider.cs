using System;

namespace Sondor.HttpClient.Tests;

/// <summary>
/// The test time provider.
/// </summary>
public class TestTimeProvider :
    TimeProvider
{
    /// <inheritdoc />
    public override DateTimeOffset GetUtcNow()
    {
        return new DateTimeOffset(DateTimeOffset.UtcNow.Year,
            DateTimeOffset.UtcNow.Month,
            DateTimeOffset.UtcNow.Day,
            DateTimeOffset.UtcNow.Hour,
            0,
            0,
            DateTimeOffset.UtcNow.Offset);
    }
}