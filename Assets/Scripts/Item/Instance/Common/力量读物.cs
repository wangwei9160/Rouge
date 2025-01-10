

public class 力量读物 : BaseItem 
{
    public override int ID => 8;

    public override string Name => "力量读物";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.playerAttr.attackPower += 1;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}