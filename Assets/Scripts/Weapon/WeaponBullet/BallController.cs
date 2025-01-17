using UnityEngine;

public class BallController : 直线飞行
{
    public float damage = 10f;          // 伤害
    

    public string Name;                 // 来源武器的名称

    public void ResetInfo(float dam , string name)
    {
        damage = dam;
        Name = name;
    }

    public bool isAlive = true;         // 是否可以穿透

    public void SetMoveToTarget(GameObject player , GameObject target)
    {
        gameObject.transform.position = player.transform.position;
        // 朝向目标移动
        dir = (target.transform.position - gameObject.transform.position).normalized;
        ResetY2Empty();
        var rota = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = rota;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy") && isAlive)
        {
            //bool ok = other.gameObject.GetComponent<MonsterController>().Damge(damage);
            bool ok = other.gameObject.GetComponent<BaseEnemy>().Damage(damage);
            if (!ok) return;
            isAlive = false;
            Destroy(gameObject);
            DamageUIManager.Instance.ShowDamgeText(other.gameObject.transform,(int)damage);
            UIManager.Instance.UpdateWeaponDamage(Name, (int)damage);
        }
    }

}
