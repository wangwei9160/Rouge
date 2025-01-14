using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class WeaponButtonUI : MonoBehaviour 
{
    public Image BG;
    public Image Icon;
    public int ID;
    public int Index;

    public void SetUI(int id , int idx )
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