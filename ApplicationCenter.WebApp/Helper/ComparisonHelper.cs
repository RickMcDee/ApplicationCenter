namespace ApplicationCenter.WebApp.Helper;

public static class ComparisonHelper
{
    public static T TakeNewValueIfChanged<T>(T value, T newValue, ref int updateCount)
    {
        if (EqualityComparer<T>.Default.Equals(value, newValue) is false)
        {
            updateCount++;
            return newValue;
        }

        return value;
    }
}