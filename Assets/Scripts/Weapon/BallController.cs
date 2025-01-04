using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 dir;

    public Weapon weapon;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject , 5);
        weapon = gameManager.Instance.weaponManager.getWeapon(1);
    }

    // Update is called once per frame
    void Update()
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && isAlive)
        {
            isAlive = false;
            other.gameObject.GetComponent<MonsterController>().Damge(weapon.damage);
            Destroy(gameObject);
            gameManager.Instance.damageUIManager.ShowDamgeText(other.gameObject.transform, weapon.damage);
            gameManager.Instance.uiInfoManager.UpdateWeaponDamage("Ball", weapon.damage);
        }
    }

}
