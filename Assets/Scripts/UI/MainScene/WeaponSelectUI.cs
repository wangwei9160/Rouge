using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WeaponSelectUI : MonoBehaviour 
{
    public Button CloseButton;              // 关闭按钮
    public GameObject Content;              // 武器内容
    public Button WeaponButtonPrefab;       // 武器ui预制体 

    void Start()
    {
        CloseButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        Refresh();
    }

    private void Refresh()
    {
        var dic = TplUtil.GetWeaponTplDic();
        foreach (var item in dic)
        {
            if (item.Value.Rank == 0)
            {
                Button go = Instantiate(WeaponButtonPrefab, Content.transform);
                go.GetComponent<WeaponBtnUI>().SetUI(item.Value.ID);
                go.onClick.AddListener(() =>
                {
                    EventCenter.Broadcast<int>(EventDefine.RefreshWeaponByID, item.Value.ID);
                    // 先设置后广播
                    GameManager.Instance.gameData.WeaponIDs[0] = item.Value.ID;
                    EventCenter.Broadcast(EventDefine.RefreshWeapon);
                    //gameObject.SetActive(false);
                });
            }
        }
        // 全部创建好后广播一次
        //EventCenter.Broadcast<int>(EventDefine.RefreshWeaponByID, GameManager.Instance.gameData.WeaponIDs[0]);
    }

}