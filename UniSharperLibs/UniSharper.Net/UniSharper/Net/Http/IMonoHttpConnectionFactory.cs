namespace UniSharper.Net.Http
{
    public interface IMonoHttpConnectionFactory
    {
        IMonoHttpConnection Create();
    }

    public class SimpleMonoHttpConnectionFactory<T> : IMonoHttpConnectionFactory where T : IMonoHttpConnection, new()
    {
        public IMonoHttpConnection Create()
        {
            return new T();
        }
    }
}