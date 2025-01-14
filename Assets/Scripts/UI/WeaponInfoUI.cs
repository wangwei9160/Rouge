using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    // GameObject
    public Image ItemBG;                // ͼ��ı�����ʾϡ�ж�
    public Image ItemIcon;              // ͼ��
    public TMP_Text ItemType;           // ��Ʒ����
    public TMP_Text ItemName;           // ��Ʒ��
    public Text ItemDescription;        // ��Ʒ����
    public Button MergeButton;          // �ϳɰ�ť
    public Button GcButton;             // ���հ�ť
    public TMP_Text GcText;             // ������Ϣ
    public Button CloseButton;          // �رհ�ť
    public int ID;                      // ��Ʒ���
    public int Index;                   // ����

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

    // ���Ժϳ�����
    private void TryMergeWeapon()
    {
        //Debug.Log(string.Format("��Ʒ{0} , ����{1}", ID, Index));
        if(GameManager.Instance.TryMerge(ID, Index))
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // ��������һЩ����
    public void ResetWeaponByID(int id , int idx)
    {
        ID = id;
        Index = idx;
        WeaponTplInfo item = TplUtil.GetWeaponTplDic()[ID];
        ItemBG.sprite = AssetManager.Instance.RankSprite[item.Rank];
        ItemType.text = "����";
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        GcText.text = string.Format("����(+{0})", (int)(item.Price * 0.2));
        ItemIcon.sprite = AssetManager.Instance.WeaponForShowSprite[item.Index];
    }

    public void ResetPosition(Transform bs)
    {
        gameObject.transform.position = bs.position + new Vector3(0 , 60 , 0);
    }

}