namespace Pipr
{
    public class InjectedType<T>
    {
        public static T Configure()
        {
            return default(T);
        }
    }
}