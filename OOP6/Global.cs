using System.Diagnostics.Metrics;

namespace OOP6;

public static class Global
{
    private static int counter = 0;
    public static int Counter
    {
        get
        {
            return Interlocked.Increment(ref counter);
        }
    }
}