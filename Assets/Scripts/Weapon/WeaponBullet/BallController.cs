using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 dir;
    public float damage = 10f;
    public float moveSpeed = 10f;

    public bool isAlive = true;

    protected void Start()
    {
        Destroy(gameObject , 5);
    }

    protected  void Update()
    {
        Move();
    }

    public void Move()
    {
        if (gameObject.transform.position.z <= -15 || gameObject.transform.position.x <= -11 || gameObject.transform.position.x >= 11)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void SetMoveToTarget(GameObject player , GameObject target)
    {
        gameObject.transform.position = player.transform.position;
        // 朝向目标移动
        dir = (target.transform.position - gameObject.transform.position).normalized;
        var rota = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = rota;
    }

    public void ResetInfo(float dam)
    {
        damage = dam;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && isAlive)
        {
            bool ok = other.gameObject.GetComponent<MonsterController>().Damge(damage);
            if (!ok) return;
            isAlive = false;
            Destroy(gameObject);
            DamageUIManager.Instance.ShowDamgeText(other.gameObject.transform,(int)damage);
            UIManager.Instance.UpdateWeaponDamage("Ball", (int)damage);
        }
    }

}
