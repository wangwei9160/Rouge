

public class 绿苹果 : BaseItem 
{
    public override int ID => 2;

    public override string Name => "绿苹果";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.playerAttr.maxHp += 10;
        GameManager.Instance.gameData.curHp += 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}