using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DamageCountUI : MonoBehaviour 
{
    public Button closeButton;
    public GameObject WeaponDamageItemPrefab;
    public GameObject scrollContent;
    private float mCurrentTime = 0;
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

    private void Update()
    {
        mCurrentTime += Time.deltaTime;
        if (mCurrentTime > 0.5f)
        {
            SortChildrenByDamage();
        } 
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

    public void SortChildrenByDamage()
    {
        int num = scrollContent.transform.childCount;
        var children = new Transform[num];
        for (int i = 0; i < num; i++)
        {
            children[i] = scrollContent.transform.GetChild(i);
        }

        var sortedChildren = children.OrderByDescending(x =>
        {
            return x.GetComponent<WeaponDemageShowUI>().Damage;
        }).ToList();
        for(int i = 0 ; i < num ; i++)
        {
            sortedChildren[i].SetSiblingIndex(i);
        }
    }

}