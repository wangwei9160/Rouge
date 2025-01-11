using TMPro;
using UnityEngine;

public class ��ͨ�� : BaseWeapon
{

    public float ���� = 0.5f;

    public override float attack => weaponInfo.Attack;
    public override float attackSpeed => weaponInfo.AttackSpeed;    // �������

    public GameObject go; // ���е���

    protected override void Attack()
    {
        GameObject tg = EnemyGenerator.Instance.GetEnemy();
        if(tg == null)
        {
            return;
        }
        GameObject tmp = Instantiate(go, transform);
        tmp.GetComponent<BallController>().SetMoveToTarget(gameObject,tg);
        tmp.GetComponent<BallController>().ResetInfo(attack , "��ͨ��");
    }
}