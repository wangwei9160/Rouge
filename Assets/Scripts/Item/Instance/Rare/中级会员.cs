

public class 中级会员 : BaseItem 
{
    public override int ID => 5;

    public override string Name => "中级会员";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.OnGetFreeCnt(2);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}