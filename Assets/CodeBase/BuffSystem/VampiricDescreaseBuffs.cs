namespace CodeBase.BuffSystem
{
    public class VampiricDescreaseBuffs : Buffs 
    {
        public override void Buff()
        {
            _creature.VampiricDebuffEnemy += 25;
        }
    }
}