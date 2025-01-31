public interface IPool<T>
{
    T Get();
    void Release(T t);
}
