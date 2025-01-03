using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AwardUIManager : MonoBehaviour 
{
    // 商店按钮
    public Button StoreButton;

    // 奖励信息
    public GameObject awardPrefab;

    public GameObject ScrollViewContent;

    public bool isSet;

    void Awake()
    {
        Myinit();
    }

    void Start()
    {
        Myinit();
        StoreButton.onClick.AddListener(goStore);
    }

    void Myinit()
    {
        isSet = false;
        StoreButton.gameObject.SetActive(false);
    }

    private void goStore()
    {
        GameContext.isGameOver = false;
        SceneManager.LoadScene("StoreScene");
    }

    void Update()
    {
        if (GameContext.isGameOver)
        {
            if(!isSet)
            {
                StoreButton.gameObject.SetActive(true);
                isSet = true;
                ClearAndAddElements();

            }
           
            return;
        }
    }

    private void ClearAndAddElements()
    {
        foreach (Transform item in ScrollViewContent.transform)
        {
            Destroy(item.gameObject);
        }

        var vis = 0;
        for(int i = 0; i < 3; i++)
        {
            GameObject tmp = Instantiate(awardPrefab, ScrollViewContent.transform);
            int tp = GetRand(vis);
            vis += (1 << tp);
            tmp.GetComponent<AwardInfo>().SetAward(1,tp );
        }
    }

    public int GetRand(int key)
    {
        while(true)
        {
            int rd = Random.Range(0, 3);
            if((key & (1 << rd)) == 0)
            {
                return rd;
            }
        }
    }
}