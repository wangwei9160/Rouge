

public class 橙子 : BaseItem 
{
    public override int ID => 3;
    public override string Name => "橙子";

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