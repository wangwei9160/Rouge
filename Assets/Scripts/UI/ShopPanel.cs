using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour 
{
    public GameManager gameManager;
    // 商店按钮
    public Button GoButton;

    // 奖励信息
    public GameObject awardPrefab;

    public GameObject ScrollViewContent;

    void Awake()
    {
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GoButton.onClick.AddListener(nextLevel);
    }

    // 下一关
    private void nextLevel()
    {
        GameContext.CurrentLevel += 1;
        GameContext.number.res = 0;
        gameManager.Instance.TransState(StateID.FightState);
    }

    void Update()
    {
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