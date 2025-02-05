

public class 普通会员 : BaseItem 
{
    public override int ID => 4;

    public override string Name => "普通会员";

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.OnGetFreeCnt(1);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }
}