using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public GameObject Weapons;  // 所有已有的武器
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
                        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[gd.WeaponIDs[i]];// 获得这把武器的信息

                        GameObject go = Instantiate(AssetManager.Instance.WeaponPrefabs[info.Index], Weapons.transform);
                        go.GetComponent<BaseWeapon>().InitID(info.ID);

                    }
                }
            }
            mCurrentSecond += Time.deltaTime;
            if(mCurrentSecond >= 1f)
            {
                mCurrentSecond = 0;
                #region 橙子效果
                if (GameManager.Instance.gameData.HasItem((int)Item.橙子))
                {
                    GameManager.Instance.OnMoneyChange(GameManager.Instance.gameData.HasItemCount((int)Item.橙子));
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
