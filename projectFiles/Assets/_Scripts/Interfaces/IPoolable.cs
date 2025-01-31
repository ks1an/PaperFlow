public interface IPoolable<T>
{
    void Init(System.Action<T> returnAction);
    void ReturnToPool();
}
