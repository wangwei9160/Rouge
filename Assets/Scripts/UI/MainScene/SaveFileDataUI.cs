using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveFileDataUI : MonoBehaviour 
{
    public int Index;
    public bool isLoad;
    public TMP_Text text; // 空图标
    public bool isEmpty = true;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickThis);
    }

    private void OnClickThis()
    {
        Debug.Log(string.Format("{0}了存档{1}", isLoad ? "读取" : "覆盖" , Index));
        if (isEmpty && isLoad)
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI , "当前存档为空");
            return;
        }
        //GameManager.Instance.gameData.Init();
        GameManager.Instance.gameData.SaveIndex = Index;
        EventCenter.Broadcast(EventDefine.StartGame);
        //Debug.Log("加载战斗场景");
        SceneManager.LoadScene("BattleScene");
    }
    
    public void Set(int idx , bool Load)
    {
        Index = idx;
        isLoad = Load;
        isEmpty = true;
    }

    public void Refresh()
    {

    }

}