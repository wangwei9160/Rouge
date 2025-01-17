using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;

    private float moveSpeed = 1f;
    /*
    1 move
    2 move to player
    3 attack
    4
    5 dead
    */
    private int status;
    public int Status
    {
        get { return status; }
        private set { status = value; }
    }

    private float attackRange = 3f;
    private Animator animator;
    private string currentAnimation = "";

    private float MaxHealthValue => 19f + 5 * (GameManager.Instance.gameData.CurrentWave - 1) + 10 * (GameManager.Instance.gameData.curLevel - 1);
    private float healthValue;

    public int Exp = 10;

    // Start is called before the first frame update
    void Start()
    {
        monster = gameObject;
        animator = GetComponent<Animator>();
        //monster.transform.position = new Vector3(9, 0, -15);
        player = GameObject.Find("Player").gameObject;
        healthValue = MaxHealthValue;
    }

    void Update()
    {
        StatusChange();
        Move();
    }

    private void StatusChange()
    {
        if(status == 5) // ������
        {
            return;
        }
        if (healthValue < 0)
        {
            status = 5;
            ChangeAnimation("Death");
            //Debug.Log("Broadcast , (EventDefine.OneEnemyDeath");
            EventCenter.Broadcast(EventDefine.OneEnemyDeath, gameObject);
            return;
        }

        if ( status == 0 && monster.transform.position.z >= attackRange)
        {
            // ��ǰ�ƶ�
            status = 1;
        }else
        {
            Vector3 pos1 = monster.transform.position;
            Vector3 pos2 = player.transform.position;
            float dis = Vector3.Distance(pos1, pos2);
            //Debug.Log("Distance to target: " + dis);
            if (dis <= attackRange)
            {
                status = 2;
            }
        }
    }

    private void Move()
    {
        if (status == 0)
        {
            // ��ǰ�ƶ�
            monster.transform.Translate( new Vector3(0,0,1) * moveSpeed * Time.deltaTime , Space.World);
            ChangeAnimation("Walk_F");
        }
        else if (status == 1)
        {
            // ����Ŀ���ƶ�
            var dir = (player.transform.position - monster.transform.position).normalized;
            var rota = Quaternion.LookRotation(dir);
            monster.transform.rotation = Quaternion.Slerp(transform.rotation, rota, Time.deltaTime);
            monster.transform.position += dir * moveSpeed * Time.deltaTime;
            
        }else if(status == 2)
        {
            // ����
            ChangeAnimation("Attack_01");
            
        }
        healthValue -= 1f * Time.deltaTime;
    }

    public bool Damge(float damge)
    {
        if(healthValue < 0f)
        {
            return false;
        }
        healthValue -= damge ;
        return true;
    }


    void ChangeAnimation(string animation , float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation , crossfade);
        }
    }



}
