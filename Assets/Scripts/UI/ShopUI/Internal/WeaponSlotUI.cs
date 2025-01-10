using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponSlotUI : MonoBehaviour 
{
    public TMP_Text Count;
    public GameObject Content;
    public GameObject WeaponButtonPrefab;

    public void Refresh(GameData data)
    {
        ClearAll();
        int cur = 0, n = data.WeaponSlot;
        for (int i = 0; i < n; i++)
        {
            GameObject go = Instantiate(WeaponButtonPrefab, Content.transform);
            if (data.WeaponIDs[i] != -1)
            {
                cur++;
                go.GetComponent<WeaponButtonUI>().SetUI(data.WeaponIDs[i]);
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