using System;

[Serializable]
public class �˺����ף�� : BaseBuff
{
    public override int ID => 1;

    public override void OnBeforeDamage(ref DamageInfo damage)
    {
        base.OnBeforeDamage(ref damage);
        damage.bonus += 10 * Cnt;
    }
}