using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour 
{
    public Button CloseButton;              // ¹Ø±Õ°´Å¥
    public GameObject Content;              // ÎäÆ÷ÄÚÈÝ
    public Button WeaponButtonPrefab;   // ÎäÆ÷uiÔ¤ÖÆÌå 

    void Awake()
    {
    }

    private void OnEnable()
    {
        ClearAll();
        Refresh();
    }

    void Start()
    {
        CloseButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    void Update()
    {
    }

    private void Refresh()
    {
        var dic = TplUtil.GetWeaponTplDic();
        foreach (var item in dic)
        {
            if (item.Value.Rank == 0)
            {
                Button go = Instantiate(WeaponButtonPrefab, Content.transform);
                go.GetComponent<WeaponButtonUI>().SetUI(item.Value.ID);
                go.onClick.AddListener(() =>
                {
                    GameManager.Instance.gameData.WeaponIDs[0] = item.Value.ID;
                    gameObject.SetActive(false);
                });
            }
        }
    }


    private void ClearAll()
    {
        foreach (Transform item in Content.transform)
        {
            Destroy(item.gameObject);
        }
    }

}