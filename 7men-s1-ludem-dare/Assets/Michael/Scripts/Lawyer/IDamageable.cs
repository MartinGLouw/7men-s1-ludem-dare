namespace Managers.Lawyer
{
    public interface IDamageable<T>
    {
        void TakeDamage(T value);
    }
}