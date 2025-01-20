
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveFileDataUI : MonoBehaviour 
{
    public int Index;
    public bool isLoad;
    public Text text; // ��ͼ��
    public bool isEmpty = true;

    private void OnEnable()
    {
        Refresh();   
    }

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
        GameManager.Instance.SaveOrLoadData(isLoad, Index);
    }
    
    public void Set(int idx , bool Load)
    {
        Index = idx;
        isLoad = Load;
        //Refresh();
    }

    public void Refresh()
    {
        StartCoroutine(IORefresh());
    }

    IEnumerator IORefresh()
    {
        yield return new WaitForSeconds(RandomUtil.RandomFloat(0.2f , false));
        string path = Application.persistentDataPath + string.Format("/SaveData{0}.data", Index);
        if (!System.IO.File.Exists(path))
        {
            //Debug.Log(string.Format("{0} ������", path));
            isEmpty = true;
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
            isEmpty = false;
            Debug.Log(string.Format("�����ļ�SaveData{0}.data", Index));
        }
        yield return new WaitForSeconds(0.1f);
    }

}