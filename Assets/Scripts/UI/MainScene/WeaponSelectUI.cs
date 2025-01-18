using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour 
{
    public Button CloseButton;              // ¹Ø±Õ°´Å¥
    public GameObject Content;              // ÎäÆ÷ÄÚÈÝ
    public Button WeaponButtonPrefab;   // ÎäÆ÷uiÔ¤ÖÆÌå 
    void Start()
    {
        Refresh();
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
                    EventCenter.Broadcast(EventDefine.RefreshWeapon);
                });
            }
        }
    }

}