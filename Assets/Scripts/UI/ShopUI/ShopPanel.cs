using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour 
{
    // 商店按钮
    public Button GoButton;
    public Button RefeshButton;

    // 奖励信息
    public GameObject awardPrefab;      

    public GameObject ScrollViewContent;    // 商店content

    public GameObject WeaponSlot;           // 武器槽
    public GameObject ItemSlot;             // 道具槽

    public GameObject playerAttr;           // 角色属性

    public GameData GameDataInstance
    {
        get
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager instance is not set!");
                return null;
            }
            return GameManager.Instance.gameData;
        }
    }

    void Start()
    {
        GoButton.onClick.AddListener(nextWave);
        RefeshButton.onClick.AddListener(RefreshAll);
    }

    private void OnEnable()
    {
        EventCenter.AddListener(EventDefine.RefreshWeapon, RefreshWeapon);
        EventCenter.AddListener(EventDefine.RefreshItem, RefreshItem);
        RefreshAll();
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener(EventDefine.RefreshWeapon, RefreshWeapon);
        EventCenter.RemoveListener(EventDefine.RefreshItem, RefreshItem);
    }

    // 下一关
    private void nextWave()
    {
        GameContext.number.res = 0;
        GameManager.Instance.TransState(StateID.FightState);
        GameManager.Instance.gameData.CurrentWave += 1;
        EventCenter.Broadcast(EventDefine.HideShopUI);
    }

    public void RefreshAll()
    {
        RefreshAllShopItem();
        RefreshPlayerAttribute();
        RefreshBagSlot();
        RefreshWeaponSlot();
    }

    public void RefreshWeapon()
    {
        RefreshPlayerAttribute();
        RefreshWeaponSlot();
    }

    public void RefreshItem()
    {
        RefreshPlayerAttribute();
        RefreshBagSlot();
    }

    public void RefreshAllShopItem()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject go = ScrollViewContent.transform.GetChild(i).gameObject;
            GameObject item = go.transform.GetChild(0).gameObject;
            int WeaponOrItem = Random.Range(0, 1+1);
            if(WeaponOrItem == 0)
            {
                int rd = Random.Range(1, 10 + 1);
                WeaponTplInfo info = TplUtil.GetWeaponTplDic()[rd];
                go.name = string.Format("weapon-{0}-{1}", info.ID, info.Name);
                item.GetComponent<BuyItemScript>().ResetWeapon(info);
            }else
            {
                int rd = Random.Range(1, 9 + 1);
                ItemTplInfo info = TplUtil.GetItemTplDic()[rd];
                go.name = string.Format("item-{0}-{1}", info.ID, info.Name);
                item.GetComponent<BuyItemScript>().ResetItem(info);
            }
            
            item.SetActive(true);// 确保刷新出来
        }
    }

    public void RefreshPlayerAttribute()
    {
        playerAttr.GetComponent<PlayerAttributeUI>().Refresh(GameDataInstance);
    }

    public void RefreshWeaponSlot()
    {
        WeaponSlot.GetComponent<WeaponSlotUI>().Refresh(GameDataInstance);
    }

    public void RefreshBagSlot()
    {
        ItemSlot.GetComponent<ItemSlotUI>().Refresh(GameDataInstance);
    }

}