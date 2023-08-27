namespace CodeBase.BuffSystem
{
    public class DoubleDamageBuffs : Buffs 
    {
        public override void Buff()
        {
            _creature.CurrentDamage *= 2;
        }
    }
}