using System;

[Serializable]
public class ����ף�� : BaseBuff
{
    public override int ID => 2;

    public override void OnBeforeHurt(ref DamageInfo damage)
    {
        base.OnBeforeHurt(ref damage);
        damage.reduction += Cnt;
    }

}