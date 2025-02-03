using UnityEngine;

public class 卡牌 : BaseWeapon
{
    public float 倍率 = 0.5f;


    public override float attack => weaponInfo.Attack;
    public override float attackSpeed => weaponInfo.AttackSpeed;    // 攻击间隔

    public GameObject go; // 飞行道具

    protected override void Attack()
    {
        GameObject tg = EnemyGenerator.Instance.GetEnemy();
        if (tg == null)
        {
            return;
        }
        InitFlyingBullet(tg);
    }

    private void InitFlyingBullet(GameObject target)
    {
        Vector3 v3 = target.transform.position;
        float dis = Vector3.Distance(gameObject.transform.position, v3);    
        for(int rota = -1; rota <= 1; rota++)
        {
            GameObject go_mid = Instantiate(go, transform);
            go_mid.GetComponent<BallController>().SetMove(gameObject.transform.position, v3 + new Vector3(dis / 2 * (rota), 0 ,0));
            go_mid.GetComponent<BallController>().ResetInfo(attack, ID);
        }
        
    }
}