using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemScript : MonoBehaviour 
{
    // GameObject
    public Image ItemBG;                // 图标的背景表示稀有度
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
    public bool isItem = true;          // 判断物品类型
    public int ID;                      // 物品编号

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
                    // 购买失败
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
        ID = item.ID;
        ItemBG.sprite = AssetManager.Instance.RankSprite[item.Rank];
        ItemType.text = "道具";
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
        ItemType.text = "武器";
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        ItemGold.text = item.Price.ToString();
        ItemIcon.sprite = AssetManager.Instance.WeaponForShowSprite[item.Index];
        isItem = false;
    }

}