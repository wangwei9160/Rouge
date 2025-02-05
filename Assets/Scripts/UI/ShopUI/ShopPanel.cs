using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour 
{
    // �̵갴ť
    public Button GoButton;
    public Button RefeshButton;
    public Text RefreshText;
    public Button BackBtn;

    // ������Ϣ
    public GameObject awardPrefab;      

    public GameObject ScrollViewContent;    // �̵�content

    public GameObject WeaponSlot;           // ������
    public GameObject ItemSlot;             // ���߲�

    public GameObject playerAttr;           // ��ɫ����

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

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.RefreshWeapon, RefreshWeapon);
        EventCenter.AddListener(EventDefine.RefreshItem, RefreshItem);
        EventCenter.AddListener(EventDefine.ShowShopUI, RefreshAll);
        EventCenter.AddListener(EventDefine.OnGetFree, RefreshButtonUI);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.RefreshWeapon, RefreshWeapon);
        EventCenter.RemoveListener(EventDefine.RefreshItem, RefreshItem);
        EventCenter.RemoveListener(EventDefine.ShowShopUI, RefreshAll);
        EventCenter.RemoveListener(EventDefine.OnGetFree, RefreshButtonUI);
    }

    void Start()
    {
        GoButton.onClick.AddListener(nextWave);
        RefeshButton.onClick.AddListener(RefreshAll);
        RefreshButtonUI();
        BackBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveGameData(GameManager.Instance.gameData.SaveIndex);
            SceneManager.LoadScene("MainScene");
            GameManager.Instance.gameData = new GameData();
        });
    }

    // ��һ��
    private void nextWave()
    {
        GameManager.Instance.gameData.CurrentWave += 1;
        GameManager.Instance.DoBeforStartGame();
        EventCenter.Broadcast(EventDefine.RefreshEnemyCount);
        EventCenter.Broadcast(EventDefine.HideShopUI);
    }

    public void RefreshButtonUI()
    {
        RefreshText.text = GameManager.Instance.gameData.freeTime > 0 ? "���" : string.Format("ˢ�� -{0}",GameManager.Instance.gameData.refreshCnt);
    }

    public void RefreshAll()
    {
        int need = GameManager.Instance.gameData.freeTime > 0 ? 0 : GameManager.Instance.gameData.refreshCnt;
        if (GameManager.Instance.TryPayMoney(need))
        {
            GameManager.Instance.OnMoneyChange(-1 * need);
            if(need == 0)
            {
                GameManager.Instance.gameData.freeTime -= 1;
            }else
            {
                GameManager.Instance.gameData.refreshCnt++;
            }
        }
        else
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI , "��Ҳ���");
            return;
        }
        RefreshButtonUI();
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
                // ����
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
                // ����
                List<int> weaponList = TplUtil.GetWeaponTplDic().Values
                    .Where(obj => obj.Rank == RankType )
                    .Select(obj => obj.ID)
                    .ToList();
                int rd = RandomUtil.GetRandomValueInList(weaponList);
                WeaponTplInfo info = TplUtil.GetWeaponTplDic()[rd];
                go.name = string.Format("weapon-{0}-{1}", info.ID, info.Name);
                item.GetComponent<BuyItemScript>().ResetWeapon(info);
            }
            item.SetActive(true);// ȷ��ˢ�³���
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