using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject Ball;         // 子弹预制体

    //public Weapon Weapon;

    public GameObject Weapons;  // 所有已有的武器
    public bool isInit = false;

    private float mCurrentSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Weapon = WeaponManager.Instance.getWeapon(1);

    }

    // Update is called once per frame
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
        //mCurrentSecond += Time.deltaTime;

        //if(mCurrentSecond > Weapon.colddown)
        //{
        //    mCurrentSecond = 0;
        //    GameObject enemy = EnemyGenerator.Instance.GetEnemy();
        //    if (enemy != null)
        //    {
        //        GameObject ball = GameObject.Instantiate(Ball);
        //        ball.GetComponent<BallController>().SetMoveToTarget(gameObject , enemy);
        //    }
            
        //}

    }
}
