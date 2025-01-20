using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataFileUI : MonoBehaviour 
{
    public Transform Content;

    public TMP_Text Notice;
    public Button BackButton;

    private bool isLoad;

    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.ShowDataFileUI, Show);
        EventCenter.AddListener(EventDefine.HideDataFileUI, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.ShowDataFileUI, Show);
        EventCenter.RemoveListener(EventDefine.HideDataFileUI, Hide);
    }

    private void Start()
    {
        BackButton.onClick.AddListener(() =>
        {
            if(isLoad)
            {
                EventCenter.Broadcast(EventDefine.ShowMainScene);
            }else
            {
                EventCenter.Broadcast(EventDefine.ShowSelectUI);
            }
            Hide();
        });
        //Init();
        Hide();
    }

    private void Init()
    {
        for(int i = 0; i < Constants.FILEDATA_SLOT_MAX; i++)
        {
            GameObject tmp = Instantiate(AssetManager.Instance.SaveFileDataTpl, Content);
            tmp.GetComponent<SaveFileDataUI>().Set(i, isLoad);
        }
    }

    private void Refresh()
    {
        for (int i = 0; i < Constants.FILEDATA_SLOT_MAX; i++)
        {
            var tmp = Content.GetChild(i);
            tmp.GetComponent<SaveFileDataUI>().Set(i, isLoad);

        }
    }

    private void SaveOrLoad(int idx)
    {
        GameManager.Instance.gameData.SaveIndex = idx;
        if(isLoad)
        {

        }
    }

    private void Show(int Load)
    {
        isLoad = Load == 1; // 1表示读取， 0 表示保存
        Notice.text = string.Format("请选择<color=#f98134>{0}</color>的存档", isLoad ? "读取" : "覆盖");
        gameObject.SetActive(true);
        Refresh();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}