using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemScript : MonoBehaviour 
{
    // GameObject
    public Image ItemIcon;              // 图标
    public TMP_Text ItemType;           // 商品类型
    public TMP_Text ItemName;           // 商品名
    public Text ItemDescription;        // 商品描述
    public TMP_Text ItemLimit;          // 商品购买限制
    public Button buyButton;            // 购买按钮
    public TMP_Text ItemGold;           // 商品价格
    public Image LockImage;             // 商品锁定图标
    public Button LockButton;           // 锁定按钮
    public TMP_Text LockText;           // 锁定解锁文字
    public bool isLock = false;         // 锁定检测

    private void Start()
    {
        ItemLimit.gameObject.SetActive(false);
        buyButton.onClick.AddListener(() =>
        {
            Debug.Log("Buy " + ItemName.text);
            gameObject.SetActive(false);
        });

        LockButton.onClick.AddListener(() =>
        {
            isLock = !isLock;
            LockImage.gameObject.SetActive(isLock);
            if (isLock)
            {
                LockText.text = "解锁";
            }
            else
            {
                LockText.text = "锁定";
            }
        });
    }

    public void ResetItem(ItemTplInfo item)
    {
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        ItemGold.text = item.Price.ToString();
        ItemIcon.sprite = AssetManager.Instance.itemSprite[item.Index];
    }

}