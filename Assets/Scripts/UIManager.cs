using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    // �ؿ�UI
    public Text mCurrentLevel;      // ��ǰ����
    public Text EnemyCount;         // ���˼���

    private Action EnemyNumberUpdate;   
    private Action GameOverUI;

    // �����˺����UI
    public Button DamageCountUIButton;
    public GameObject DamageCount;

    private Dictionary<string, int> WeaponDamage = new Dictionary<string, int>();

    // �̵�UI
    public GameObject ShopUI;

    void Awake()
    {
        Myinit();
    }

    void Start()
    {
        Myinit();
        GameContext.number = new EnemyNumber(0, 7);
        DamageCountUIButton.onClick.AddListener(() =>
        {
            DamageCount.SetActive(!DamageCount.activeInHierarchy);
        });
    }

    public void Myinit()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        
        ShopUI.SetActive(false);
    }

    void Update()
    {
        if (gameManager.Instance.state == StateID.ShopState)
        {
            ShopUI.SetActive(true);
            mCurrentLevel.gameObject.SetActive(false);
            EnemyCount.gameObject.SetActive(false);
            return;
        }
        if(gameManager.Instance.state == StateID.FightState)
        {
            UpdateCurrentLevel();
            ShopUI.SetActive(false);
            mCurrentLevel.gameObject.SetActive(true);
            EnemyCount.gameObject.SetActive(true);
        }
        if (EnemyNumberUpdate != null)
        {
            EnemyNumberUpdate();
        }
    }

    // ���������˺�
    public void UpdateWeaponDamage(string name, int value)
    {
        if (WeaponDamage.ContainsKey(name))
        {
            WeaponDamage[name] += value;
        }else
        {
            WeaponDamage.Add(name, value);
        }
    }

    public Dictionary<string,int> allWeaponDamage()
    {
        return WeaponDamage;
    }

    public void UpdateCurrentLevel()
    {
        mCurrentLevel.text = "�� " + GameContext.CurrentLevel + " ��";
    }

    // ���µ�������
    public void UpdateEnemyNumber(int num)
    {
        GameContext.number.res += num;
        EnemyNumberUpdate += UpdateEnemy;
    }

    private void UpdateEnemy()
    {
        EnemyCount.text = GameContext.number.res.ToString() + " / " + GameContext.number.total.ToString();
    }

}