namespace CodeBase.BuffSystem
{
    public class ArmorDestructionBuffs : Buffs 
    {
        public override void Buff()
        {
            _creature.ArmorDestruction += 10;
        }
    }
}