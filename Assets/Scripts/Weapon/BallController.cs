using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallController : BaseWeapon
{
    public Vector3 dir;

    public Weapon weapon;

    public bool isAlive = true;

    protected override void Start()
    {
        Destroy(gameObject , 5);
        weapon = WeaponManager.Instance.getWeapon(1);
    }

    protected override void Update()
    {
        Move();
    }

    public void Move()
    {
        if (gameObject.transform.position.z <= -15 || gameObject.transform.position.x <= -11 || gameObject.transform.position.x >= 11)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += dir * weapon.moveSpeed * Time.deltaTime;
    }

    public void SetMoveToTarget(GameObject player , GameObject target)
    {
        gameObject.transform.position = player.transform.position;
        // 朝向目标移动
        dir = (target.transform.position - gameObject.transform.position).normalized;
        var rota = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = rota;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && isAlive)
        {
            bool ok = other.gameObject.GetComponent<MonsterController>().Damge(weapon.damage);
            if (!ok) return;
            isAlive = false;
            
            Destroy(gameObject);
            DamageUIManager.Instance.ShowDamgeText(other.gameObject.transform, weapon.damage);
            UIManager.Instance.UpdateWeaponDamage("Ball", weapon.damage);
        }
    }

}
