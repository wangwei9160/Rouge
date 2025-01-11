using TMPro;
using UnityEngine;

public class 普通剑 : BaseWeapon
{

    public float 倍率 = 0.5f;

    public override float attack => weaponInfo.Attack;
    public override float attackSpeed => weaponInfo.AttackSpeed;    // 攻击间隔

    public GameObject go; // 飞行道具

    protected override void Attack()
    {
        GameObject tg = EnemyGenerator.Instance.GetEnemy();
        if(tg == null)
        {
            return;
        }
        GameObject tmp = Instantiate(go, transform);
        tmp.GetComponent<BallController>().SetMoveToTarget(gameObject,tg);
        tmp.GetComponent<BallController>().ResetInfo(attack , "普通剑");
    }
}