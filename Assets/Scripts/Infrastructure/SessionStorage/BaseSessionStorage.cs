namespace Infrastructure.SessionStorage
{
    public class BaseSessionStorage<T>
    {
        public T Data { get; protected set; }

        public virtual void UpdateStorage(T data)
        {
            Data = data;
        }
    }
}