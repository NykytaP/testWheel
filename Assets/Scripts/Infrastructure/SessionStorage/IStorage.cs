namespace Infrastructure.SessionStorage
{
    public interface IStorage<T>
    {
        T Data { get; }
        
        void UpdateStorage(T data);
    }
}