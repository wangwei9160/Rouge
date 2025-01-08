using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour 
{
    // �̵갴ť
    public Button GoButton;
    public Button RefeshButton;

    // ������Ϣ
    public GameObject awardPrefab;      

    public GameObject ScrollViewContent;    // �̵�content

    public GameObject playerAttr;            // ��ɫ����

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

    // ��һ��
    private void nextWave()
    {
        GameContext.CurrentLevel += 1;
        GameContext.number.res = 0;
        GameManager.Instance.TransState(StateID.FightState);
    }

    private void RefreshAll()
    {
        RefreshAllShopItem();
        RefreshPlayerAttribute();
    }

    public void RefreshAllShopItem()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject go = ScrollViewContent.transform.GetChild(i).gameObject;
            GameObject item = go.transform.GetChild(0).gameObject;
            int rd = Random.Range(1, 3 + 1);
            ItemTplInfo info = TplUtil.GetItemTplDic()[rd];
            go.name = string.Format("item-{0}-{1}", info.ID, info.Name);
            item.GetComponent<BuyItemScript>().ResetItem(info);
            item.SetActive(true);// ȷ��ˢ�³���
        }
    }

    public void RefreshPlayerAttribute()
    {
        playerAttr.GetComponent<PlayerAttributeUI>().Refresh(GameDataInstance);
    }

    //private void ClearAndAddElements()
    //{
    //    foreach (Transform item in ScrollViewContent.transform)
    //    {
    //        Destroy(item.gameObject);
    //    }

    //    var vis = 0;
    //    for(int i = 0; i < 3; i++)
    //    {
    //        GameObject tmp = Instantiate(awardPrefab, ScrollViewContent.transform);
    //        int tp = GetRand(vis);
    //        vis += (1 << tp);
    //        tmp.GetComponent<AwardInfo>().SetAward(1,tp );
    //    }
    //}

    //public int GetRand(int key)
    //{
    //    while(true)
    //    {
    //        int rd = Random.Range(0, 3);
    //        if((key & (1 << rd)) == 0)
    //        {
    //            return rd;
    //        }
    //    }
    //}
}