namespace CodeBase.BuffSystem
{
    public class UpArmorBuffs : Buffs 
    {
        public override void Buff()
        {
            _creature.Armor += 50;
        }
    }
}