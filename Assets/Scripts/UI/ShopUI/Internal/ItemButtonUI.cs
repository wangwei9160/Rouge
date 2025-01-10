using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonUI : MonoBehaviour 
{
    public Image BG;
    public Image Icon;
    public Text Count;

    public void SetItemUI(int id , int cnt)
    {
        ItemTplInfo info = TplUtil.GetItemTplDic()[id];
        BG.sprite = AssetManager.Instance.RankSprite[info.Rank];
        Icon.color = Color.white;
        Icon.sprite = AssetManager.Instance.itemSprite[info.Index];
        if(cnt > 1)
        {
            Count.gameObject.SetActive(true);
        }
        Count.text = cnt.ToString();
    }

}