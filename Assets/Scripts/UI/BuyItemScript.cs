using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemScript : MonoBehaviour 
{
    // GameObject
    public Image ItemIcon;              // ͼ��
    public TMP_Text ItemType;           // ��Ʒ����
    public TMP_Text ItemName;           // ��Ʒ��
    public Text ItemDescription;        // ��Ʒ����
    public TMP_Text ItemLimit;          // ��Ʒ��������
    public Button buyButton;            // ����ť
    public TMP_Text ItemGold;           // ��Ʒ�۸�
    public Image LockImage;             // ��Ʒ����ͼ��
    public Button LockButton;           // ������ť
    public TMP_Text LockText;           // ������������
    public bool isLock = false;         // �������

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
                LockText.text = "����";
            }
            else
            {
                LockText.text = "����";
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