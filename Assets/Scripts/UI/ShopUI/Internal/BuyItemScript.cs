using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemScript : MonoBehaviour 
{
    // GameObject
    public Image ItemBG;                // ͼ��ı�����ʾϡ�ж�
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
    public bool isItem = true;          // �ж���Ʒ����
    public int ID;                      // ��Ʒ���

    private void Start()
    {
        ItemLimit.gameObject.SetActive(false);
        buyButton.onClick.AddListener(() =>
        {
            if (isItem) GameManager.Instance.BuyItemByID(ID);
            else
            {
                if (!GameManager.Instance.BuyWeaponByID(ID))
                {
                    // ����ʧ��
                    return;
                }
            }
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
        ID = item.ID;
        ItemBG.sprite = AssetManager.Instance.RankSprite[item.Rank];
        ItemType.text = "����";
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        ItemGold.text = item.Price.ToString();
        ItemIcon.sprite = AssetManager.Instance.itemSprite[item.Index];
        isItem = true;
    }

    public void ResetWeapon(WeaponTplInfo item)
    {
        ID = item.ID;
        ItemBG.sprite = AssetManager.Instance.RankSprite[item.Rank];
        ItemType.text = "����";
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        ItemGold.text = item.Price.ToString();
        ItemIcon.sprite = AssetManager.Instance.WeaponForShowSprite[item.Index];
        isItem = false;
    }

}