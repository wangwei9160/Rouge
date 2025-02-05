

public class 超级会员 : BaseItem 
{
    public override int ID => 7;

    public override string Name => "超级会员";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.OnGetFreeCnt(3);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}