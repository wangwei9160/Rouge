using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotUI : MonoBehaviour 
{
    public GameObject ItemPrefab;

    public GameObject Content;

    public void Refresh(GameData data)
    {
        ClearAll();
        for(int i = 0; i < data.ItemIDs.Count; i++)
        {
            GameObject go = Instantiate(ItemPrefab , Content.transform);
            go.GetComponent<ItemButtonUI>().SetItemUI(data.ItemIDs[i], data.ItemCount[i]);
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