using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ÎäÆ÷²ÛµÄUI
/// </summary>
public class WeaponSlotUI : MonoBehaviour 
{
    
    public TMP_Text Count;
    public GameObject Content;
    public Button WeaponButtonPrefab;
    public GameObject WeaponInfo;

    private void Start()
    {
        WeaponInfo.SetActive(false);
    }

    public void Refresh(GameData data)
    {
        ClearAll();
        int cur = 0, n = data.WeaponSlot;
        for (int i = 0; i < n; i++)
        {
            Button go = Instantiate(WeaponButtonPrefab, Content.transform);
            if (data.WeaponIDs[i] != -1)
            {
                cur++;
                go.GetComponent<WeaponButtonUI>().SetUI(data.WeaponIDs[i] , i);
                go.onClick.AddListener(() =>
                {
                    //Debug.Log("ÏÔÊ¾ÎäÆ÷ÐÅÏ¢");
                    WeaponInfo.SetActive(true);
                    WeaponInfo.GetComponent<WeaponInfoUI>().ResetPosition(go.gameObject.transform);
                    WeaponInfo.GetComponent<WeaponInfoUI>().ResetWeaponByID(go.GetComponent<WeaponButtonUI>().ID , go.GetComponent<WeaponButtonUI>().Index);
                });
            }
        }
        Count.text = string.Format("ÎäÆ÷({0}/{1})", cur, n);
    }


    private void ClearAll()
    {
        foreach (Transform item in Content.transform)
        {
            Destroy(item.gameObject);
        }
    }
}