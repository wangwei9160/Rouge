using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoPannelUI : MonoBehaviour
{
    public int INDEX;
    public Button button;

    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.ShowPanelByIDInSelectPannel, Show);
    }
    public void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.ShowPanelByIDInSelectPannel, Show);
    }

    private void Start()
    {
        INDEX = 0;
        button.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowPanelByIDInSelectPannel, INDEX);
        });
        gameObject.SetActive(false);
    }

    public void Show(int idx)
    {
        if (INDEX == idx)
        {
            Color color = new Color(1f, 0.16f, 0f);
            button.GetComponent<Image>().color = color;
            gameObject.SetActive(true);
        }
        else
        {
            Color color = new Color(1f, 1f, 1f);
            button.GetComponent<Image>().color = color;
            gameObject.SetActive(false);
        }
    }
}