using System;

[Serializable]
public class �غ�����ף�� : BaseBuff
{
    public override int ID => 3;

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.OnMoneyChange(50 * Cnt);
    }

}