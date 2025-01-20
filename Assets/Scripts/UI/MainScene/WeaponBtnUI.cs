using UnityEngine;
using UnityEngine.UI;

public class WeaponBtnUI : MonoBehaviour 
{
    public Image BG;
    public Image Icon;
    public int ID;
    public int Index;

    private void OnEnable()
    {
        EventCenter.AddListener<int>(EventDefine.RefreshWeaponByID, ChangeSelectStatus);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<int>(EventDefine.RefreshWeaponByID, ChangeSelectStatus);
    }

    public void ChangeSelectStatus(int id)
    {
        BG.sprite = AssetManager.Instance.Ñ¡ÔñÎäÆ÷[(int)(id == ID ? 1 : 0)];
    }

    public void SetUI(int id, int idx)
    {
        ID = id;
        Index = idx;
        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[id];
        BG.sprite = AssetManager.Instance.RankSprite[info.Rank];
        Icon.color = Color.white;
        Icon.sprite = AssetManager.Instance.WeaponForShowSprite[info.Index];
    }

    public void SetUI(int id)
    {
        ID = id;
        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[id];
        BG.sprite = AssetManager.Instance.RankSprite[info.Rank];
        Icon.color = Color.white;
        Icon.sprite = AssetManager.Instance.WeaponForShowSprite[info.Index];
    }
}