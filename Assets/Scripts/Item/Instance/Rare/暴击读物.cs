

public class 暴击读物 : BaseItem 
{
    public override int ID => 9;

    public override string Name => "暴击读物";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.playerAttr.CriticalHitRate += 10;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}