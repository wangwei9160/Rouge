

public class 红苹果 : BaseItem 
{
    public override int ID => 1;
    public override string Name => "红苹果";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.playerAttr.maxHp += 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}