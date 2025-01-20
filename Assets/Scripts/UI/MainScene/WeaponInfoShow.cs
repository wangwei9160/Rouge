using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoShow : MonoBehaviour 
{
    public Image Icon;
    public TMP_Text Type;
    public TMP_Text Name;
    public Text Description;

    public void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.RefreshWeaponByID, RefreshWeaponByID);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.RefreshWeaponByID, RefreshWeaponByID);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void RefreshWeaponByID(int id)
    {
        gameObject.SetActive(true);
        WeaponTplInfo weapon = TplUtil.GetWeaponTplDic()[id];
        if (weapon != null)
        {
            Icon.sprite = AssetManager.Instance.WeaponForShowSprite[weapon.Index];
            Type.text = "ÎäÆ÷";
            Name.text = weapon.Name;
            Description.text = weapon.Description;
        }
    }
}