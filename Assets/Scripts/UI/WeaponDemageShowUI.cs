using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDemageShowUI : MonoBehaviour
{
    public TMP_Text weapon;
    public TMP_Text damge;
    public Image image;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateDamge(string _text)
    {
        damge.text = _text;
    }

    public void SetWeapon(string weapon_name , int damage)
    {
        weapon.text = weapon_name;
        damge.text = damage.ToString();
    }

}
