using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour 
{

    // 标识武器的ID，用于初始化武器
    public virtual int ID { get; private set; }
    public virtual void InitID(int id)
    {
        this.ID = id;
    }

    protected RankType type = RankType.Unknown;

    protected WeaponTplInfo weaponInfo
    {
        get
        {
            return TplUtil.GetWeaponTplDic()[ID];

        }
    }

    protected virtual float attackRange { get; }        // 攻击范围
    
    public virtual float attack { get; }                 // 攻击力
    public virtual float attackSpeed { get; }           // 攻击间隔
    protected float mCurrentSecond = 0;                 // 当前攻击间隔

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        
    }

    

    protected virtual void Update()
    {
        mCurrentSecond += Time.deltaTime;

        if(mCurrentSecond > attackSpeed)
        {
            mCurrentSecond = 0;
            StartCoroutine(WaitForAttack());
            //Attack();
        }
    }

    IEnumerator WaitForAttack()
    {
        float t = Random.Range(0f , 0.1f);
        yield return new WaitForSeconds(t);
        Attack();
    }

    protected virtual void Attack() { }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(string.Format("Weapon {0} attack Enemy {1}", type, other.name));
        }
    }
}