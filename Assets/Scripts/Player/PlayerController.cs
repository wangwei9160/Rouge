using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject Ball;         // �ӵ�Ԥ����

    //public Weapon Weapon;

    public GameObject Weapons;  // �������е�����
    public bool isInit = false;

    //private float mCurrentSecond = 0;

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
                        WeaponTplInfo info = TplUtil.GetWeaponTplDic()[gd.WeaponIDs[i]];// ��������������Ϣ

                        GameObject go = Instantiate(AssetManager.Instance.WeaponPrefabs[info.Index], Weapons.transform);
                        go.GetComponent<BaseWeapon>().InitID(info.ID);

                    }
                }
            }
        }else
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
