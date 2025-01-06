using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallController : BaseWeapon
{
    public GameManager gameManager;
    public Vector3 dir;

    public Weapon weapon;

    public bool isAlive = true;

    protected override void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject , 5);
        weapon = gameManager.Instance.weaponManager.getWeapon(1);
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
            gameManager.Instance.damageUIManager.ShowDamgeText(other.gameObject.transform, weapon.damage);
            gameManager.Instance.uiInfoManager.UpdateWeaponDamage("Ball", weapon.damage);
        }
    }

}
