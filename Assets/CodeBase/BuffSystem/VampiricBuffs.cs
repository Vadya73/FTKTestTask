namespace CodeBase.BuffSystem
{
    public class VampiricBuffs : Buffs 
    {
        public override void Buff()
        {
            _creature.VampirismPercent += 50;
            _creature.Armor -= 25;
        }
    }
}