namespace Damageable
{
    public interface IDealDamage
    {
        int MaxDamage { get; }
        int MinDamage { get; }
        int Damage { get; }
    }
}