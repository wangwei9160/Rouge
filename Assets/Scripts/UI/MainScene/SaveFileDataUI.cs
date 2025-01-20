using UnityEngine;
using UnityEngine.UI;

public class SaveFileDataUI : MonoBehaviour 
{
    public int Index;
    public bool isLoad;
    public GameObject Info;     // 存档信息

    public Text SaveTime;
    public Text Level;
    public Text Wave;
    public Text Money;
    public Button DelButton;
    public Text PlayTime;
    public Text text;           // 空图标
    
    public bool isEmpty = true;

    private void OnEnable()
    {
        //Refresh();
    }

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
        GameManager.Instance.SaveOrLoadData(isLoad, Index);
    }
    
    public void Set(int idx , bool Load)
    {
        Index = idx;
        isLoad = Load;
        //Refresh();
        DelButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ClearGameData(Index);
            RefreshData();
        });
        RefreshData();
    }

    private string TimeFormatter(int time)
    {
        int h = time / 3600;
        int m = (time % 3600) / 60;
        int s = time % 60;

        return $"{h:D2}:{m:D2}:{s:D2}";
    }

    public void RefreshData()
    {
        FileShowData data = GameManager.Instance.LoadPlayerPrefsData(Index);
        if (data == null) // 不存在
        {
            isEmpty = true;
            Info.SetActive(false);
            text.gameObject.SetActive(true);
        }
        else
        {
            isEmpty = false;
            Info.SetActive(true);
            text.gameObject.SetActive(false);
            SaveTime.text = data.saveTime;
            Level.text = string.Format("Level : {0}" , data.level);
            Wave.text = string.Format("Wave : {0}", data.wave);
            Money.text = string.Format("Money : {0}", data.money);
            PlayTime.text = TimeFormatter(data.playTime);
        }
    }

    //public void Refresh()
    //{
    //    StartCoroutine(IORefresh());
    //}

    //IEnumerator IORefresh()
    //{
    //    yield return new WaitForSeconds(RandomUtil.RandomFloat(0.2f , false));
    //    string path = Application.persistentDataPath + string.Format("/SaveData{0}.data", Index);
    //    if (!System.IO.File.Exists(path))
    //    {
    //        //Debug.Log(string.Format("{0} 不存在", path));
    //        isEmpty = true;
    //        Info.SetActive(false);
    //        text.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        isEmpty = false;
    //        Info.SetActive(true);
    //        text.gameObject.SetActive(false);
    //        Debug.Log(string.Format("存在文件SaveData{0}.data", Index));
    //    }
    //    yield return new WaitForSeconds(0.1f);
    //}

}