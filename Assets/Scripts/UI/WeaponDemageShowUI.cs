using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDemageShowUI : MonoBehaviour
{
    public TMP_Text weapon;
    public TMP_Text damge;
    public Image BG;
    public Image Icon;
    public int ID;
    public int Damage = 0;

    private void Awake()
    {
        EventCenter.AddListener<int,int>(EventDefine.RefreshDamageByID , Refresh);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int, int>(EventDefine.RefreshDamageByID, Refresh);
    }

    public void SetWeapon(int id , int damage)
    {
        ID = id;
        Damage = damage;
        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[id];
        BG.sprite = AssetManager.Instance.RankSprite[info.Rank];
        Icon.color = Color.white;
        Icon.sprite = AssetManager.Instance.WeaponForShowSprite[info.Index];
        weapon.text = info.Name.ToString();
        damge.text = Damage.ToString();
    }

    public void Refresh(int id , int damage)
    {
        if(id != ID)
        {
            return;
        }
        Damage += damage;
        damge.text = Damage.ToString();
    }

}
