using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AwardInfo : MonoBehaviour 
{
    public GameManager gameManager;
    public Button selectButton;
    public Image image;
    public TMP_Text info;
    
    public Weapon weapon;
    public int updateType;

    public string[] message =
    {
        "伤害增加10%",
        "冷却时间减少10%",
        "飞行速度快变"
    };
    void Awake()
    {
        Myinit();
    }

    void Start()
    {
        Myinit();
    }

    public void Myinit()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(selectButton != null )
        {
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(selectAward);
        }
    }

    public void SetAward(int type , int updType)
    {
        weapon = gameManager.Instance.weaponManager.getWeapon(type);
        updateType = updType;   
        info.text = weapon.name + message[updType];
    }

    private void selectAward()
    {
        if (updateType == 0)
        {
            weapon.damage = (int)(weapon.damage * 1.1);
        }
        else if (updateType == 1)
        {
            //weapon.colddown = (float)System.Math.Round(weapon.colddown * 0.9f, 2);
            weapon.colddown *= 0.9f;
        }
        else if (updateType == 2)
        {
            weapon.moveSpeed += 1f;
        }
        gameManager.Instance.weaponManager.resetWeapon(weapon.type, weapon.damage, weapon.colddown, weapon.moveSpeed);
        SceneManager.LoadScene("StoreScene");
    }

}