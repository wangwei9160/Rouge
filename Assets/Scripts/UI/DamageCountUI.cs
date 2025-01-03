using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCountUI : MonoBehaviour 
{
    public GameManager gameManager;
    public Button closeButton;

    public GameObject WeaponDamageItemPrefab;

    public GameObject scrollContent;

    private float mCurrentSecond = 0;

    void Awake()
    {
        
    }

    public void OnEnable()
    {
        UpdateUI();
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();  
        closeButton.onClick.AddListener(()=>{
            gameObject.SetActive(false);
        });
    }

    void Update()
    {
        mCurrentSecond += Time.deltaTime;

        if(mCurrentSecond >= 1)
        {
            UpdateUI();
            mCurrentSecond = 0;
        }
    }

    private void UpdateUI()
    {
        foreach (Transform item in scrollContent.transform)
        {
            Destroy(item.gameObject);
        }
        if(gameManager.Instance != null)
        {
            var dic = gameManager.Instance.uiInfoManager.allWeaponDamage();
            foreach (var item in dic)
            {
                var it = GameObject.Instantiate(WeaponDamageItemPrefab, scrollContent.transform);
                it.GetComponent<WeaponDemageShowUI>().SetWeapon(item.Key, item.Value);
            }
        }
        
    }
}