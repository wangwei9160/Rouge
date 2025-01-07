using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Ball;         // ×Óµ¯Ô¤ÖÆÌå

    public Weapon Weapon;

    private float mCurrentSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        Weapon = WeaponManager.Instance.getWeapon(1);
    }

    // Update is called once per frame
    void Update()
    {
        mCurrentSecond += Time.deltaTime;

        if(mCurrentSecond > Weapon.colddown)
        {
            mCurrentSecond = 0;
            GameObject enemy = EnemyGenerator.Instance.GetEnemy();
            if (enemy != null)
            {
                GameObject ball = GameObject.Instantiate(Ball);
                ball.GetComponent<BallController>().SetMoveToTarget(gameObject , enemy);
            }
            
        }

    }
}
