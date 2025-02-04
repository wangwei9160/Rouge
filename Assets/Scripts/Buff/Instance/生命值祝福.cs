using System;

[Serializable]
public class ÉúÃüÖµ×£¸£ : BaseBuff
{
    public override int ID => 0;

    public float get = 0;

    public override void OnBeforeWaveStart()
    {
        base.OnBeforeWaveStart();
        get = GameManager.Instance.gameData.playerAttr.maxHp * 0.1f * Cnt;
        GameManager.Instance.gameData.playerAttr.maxHp += get;
        //Debug.Log(string.Format("maxHp = {0}" , GameManager.Instance.gameData.playerAttr.maxHp));
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.gameData.playerAttr.maxHp -= get;
    }

}