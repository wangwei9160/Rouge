using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        GameManager.Instance.gameData.CurrentWave += 1;
        GameContext.CurrentWaveKill = 0;
        GameContext.CurrentWaveCount = GameManager.Instance.WaveDic[GameManager.Instance.gameData.CurrentWave].Total;
        EventCenter.Broadcast(EventDefine.RefreshEnemyCount);
        GameManager.Instance.TransState(StateID.FightState);
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

        var p1 = Constants.GetWeaponOrItemProbabilityByLevel(GameDataInstance.curLevel);
        var p2 = Constants.GetRankTypeProbabilityByLevel(GameDataInstance.curLevel);
        for(int i = 0; i < GameDataInstance.ShopSlot; i++)
        {
            GameObject go = ScrollViewContent.transform.GetChild(i).gameObject;
            GameObject item = go.transform.GetChild(0).gameObject;
            int WeaponOrItem = RandomUtil.RandomIndexWithProbablity(p1);
            int RankType = RandomUtil.RandomIndexWithProbablity(p2);
            if (WeaponOrItem == 1)
            {
                // 道具
                List<int> itemList = TplUtil.GetItemTplDic().Values
                    .Where(obj => obj.Rank == RankType)
                    .Select(obj => obj.ID)
                    .ToList();
                int rd = RandomUtil.GetRandomValueInList(itemList);
                ItemTplInfo info = TplUtil.GetItemTplDic()[rd];
                go.name = string.Format("item-{0}-{1}", info.ID, info.Name);
                item.GetComponent<BuyItemScript>().ResetItem(info);

            }else if (WeaponOrItem == 0)
            {
                // 武器
                List<int> weaponList = TplUtil.GetWeaponTplDic().Values
                    .Where(obj => obj.Rank == RankType )
                    .Select(obj => obj.ID)
                    .ToList();
                int rd = RandomUtil.GetRandomValueInList(weaponList);
                WeaponTplInfo info = TplUtil.GetWeaponTplDic()[rd];
                go.name = string.Format("weapon-{0}-{1}", info.ID, info.Name);
                item.GetComponent<BuyItemScript>().ResetWeapon(info);
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