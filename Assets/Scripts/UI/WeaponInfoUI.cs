using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    // GameObject
    public Image ItemBG;                // 图标的背景表示稀有度
    public Image ItemIcon;              // 图标
    public TMP_Text ItemType;           // 商品类型
    public TMP_Text ItemName;           // 商品名
    public Text ItemDescription;        // 商品描述
    public Button MergeButton;          // 合成按钮
    public Button GcButton;             // 回收按钮
    public TMP_Text GcText;             // 回收信息
    public Button CloseButton;          // 关闭按钮
    public int ID;                      // 物品编号
    public int Index;                   // 索引

    private void Start()
    {
        CloseButton.onClick.AddListener(() =>
        {
            Hide();
        });
        MergeButton.onClick.AddListener(TryMergeWeapon);
        GcButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ScaleWeaponByIndex(Index);
            Hide();
        });
    }

    // 尝试合成武器
    private void TryMergeWeapon()
    {
        //Debug.Log(string.Format("物品{0} , 索引{1}", ID, Index));
        if(GameManager.Instance.TryMerge(ID, Index))
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // 用于设置一些参数
    public void ResetWeaponByID(int id , int idx)
    {
        ID = id;
        Index = idx;
        WeaponTplInfo item = TplUtil.GetWeaponTplDic()[ID];
        ItemBG.sprite = AssetManager.Instance.RankSprite[item.Rank];
        ItemType.text = "武器";
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        GcText.text = string.Format("回收(+{0})", (int)(item.Price * 0.2));
        ItemIcon.sprite = AssetManager.Instance.WeaponForShowSprite[item.Index];
    }

    public void ResetPosition(Transform bs)
    {
        gameObject.transform.position = bs.position + new Vector3(0 , 60 , 0);
    }

}