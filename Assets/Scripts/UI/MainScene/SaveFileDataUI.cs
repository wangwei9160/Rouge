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
    public TMP_Text text; // ��ͼ��
    public bool isEmpty = true;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickThis);
    }

    private void OnClickThis()
    {
        Debug.Log(string.Format("{0}�˴浵{1}", isLoad ? "��ȡ" : "����" , Index));
        if (isEmpty && isLoad)
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI , "��ǰ�浵Ϊ��");
            return;
        }
        //GameManager.Instance.gameData.Init();
        GameManager.Instance.gameData.SaveIndex = Index;
        EventCenter.Broadcast(EventDefine.StartGame);
        //Debug.Log("����ս������");
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