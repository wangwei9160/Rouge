using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DamageCountUI : MonoBehaviour 
{
    public Button closeButton;
    public GameObject WeaponDamageItemPrefab;
    public GameObject scrollContent;

    void Awake()
    {
        EventCenter.AddListener(EventDefine.HideShopUI , Clear);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.HideShopUI, Clear);
    }

    void Start()
    { 
        closeButton.onClick.AddListener(()=>{
            gameObject.SetActive(false);
        });
        Clear();
        gameObject.SetActive(false);
    }

    private void Clear()
    {
        foreach (Transform item in scrollContent.transform)
        {
            Destroy(item.gameObject);
        }
        Dictionary<int,int> tDic = new Dictionary<int,int>();
        foreach (var id in GameManager.Instance.gameData.WeaponIDs)
        {
            if (id != -1 && !tDic.ContainsKey(id))
            {
                tDic.Add(id, 1);
                var it = GameObject.Instantiate(WeaponDamageItemPrefab, scrollContent.transform);
                it.GetComponent<WeaponDemageShowUI>().SetWeapon(id, 0);
            }
        }
        gameObject.SetActive(false);
    }
}