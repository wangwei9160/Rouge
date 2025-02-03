using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class 近战公牛 : BaseEnemy 
{
    public override int ID => 1;

    public float attackRange = 3f;

    protected override void StatusChange()
    {
        if (Status >= 5) // 已死亡
        {
            return;
        }
        if (curHp <= 0)
        {
            Die();
            return;
        }
        if (Status == 0 && gameObject.transform.position.z >= attackRange)
        {
            // 向前移动
            Status = 1;
        }
        else
        {
            Vector3 pos1 = gameObject.transform.position;
            Vector3 pos2 = player.transform.position;
            float dis = Vector3.Distance(pos1, pos2);
            //Debug.Log("Distance to target: " + dis);
            if (dis <= attackRange)
            {
                Status = 2;
            }
        }
    }

    protected override void Move()
    {
        if (Status == 0)
        {
            // 向前移动
            gameObject.transform.Translate(new Vector3(0, 0, 1) * attr.移动速度 * Time.deltaTime, Space.World);
            ChangeAnimation("Walk_F");
        }
        else if (Status == 1)
        {
            // 朝向目标移动
            var dir = (player.transform.position - gameObject.transform.position).normalized;
            var rota = Quaternion.LookRotation(dir);
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rota, Time.deltaTime);
            gameObject.transform.position += dir * attr.移动速度 * Time.deltaTime;
        }
        else if (Status == 2)
        {
            // 攻击
            if (ChangeAnimation("Attack_01"))
            {
                StartCoroutine(AttackFunc());
            }

        }
        //curHp -= 1f * Time.deltaTime;
    }

    IEnumerator AttackFunc()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.OnHpChange((int)attr.攻击力);
        DamageUIManager.Instance.ShowDamgeText(player.transform, (int)attr.攻击力);
        yield return new WaitForSeconds(0.5f);
        ChangeAnimation("Idle");
    }

}