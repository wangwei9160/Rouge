using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public GameObject Weapons;  // �������е�����
    public bool isInit = false;

    private float mCurrentSecond = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.state == StateID.FightState)
        {
            if (!isInit)
            {
                foreach (Transform item in Weapons.transform)
                {
                    Destroy(item.gameObject);
                }
                isInit = true;
                GameData gd = GameManager.Instance.gameData;
                for(int i = 0; i < gd.WeaponSlot; i++)
                {
                    if (gd.WeaponIDs[i] != -1)
                    {
                        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[gd.WeaponIDs[i]];// ��������������Ϣ

                        GameObject go = Instantiate(AssetManager.Instance.WeaponPrefabs[info.Index], Weapons.transform);
                        go.GetComponent<BaseWeapon>().InitID(info.ID);

                    }
                }
            }
            mCurrentSecond += Time.deltaTime;
            if(mCurrentSecond >= 1f)
            {
                mCurrentSecond = 0;
                #region ����Ч��
                if (GameManager.Instance.gameData.HasItem((int)Item.����))
                {
                    GameManager.Instance.OnMoneyChange(GameManager.Instance.gameData.HasItemCount((int)Item.����));
                }
                #endregion
            }
        }
        else
        {
            isInit = false;
        }

    }
}
