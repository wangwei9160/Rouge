using UnityEngine.UI;
using UnityEngine;

public class WeaponButtonUI : MonoBehaviour 
{
    public Image BG;
    public Image Icon;

    public void SetUI(int id )
    {
        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[id];
        BG.sprite = AssetManager.Instance.RankSprite[info.Rank];
        Icon.color = Color.white;
        Icon.sprite = AssetManager.Instance.WeaponSprite[info.Index];
    }
}