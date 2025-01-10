using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour 
{
    public virtual int ID { get; private set; }

    protected WeaponRank type = WeaponRank.Unknown;

    protected WeaponTplInfo weaponInfo
    {
        get
        {
            return TplUtil.GetWeaponTplDic()[ID];

        }
    }

    protected virtual float attackRange { get; }        // ¹¥»÷·¶Î§
    
    public virtual float attack { get; }                 // ¹¥»÷Á¦
    public virtual float attackSpeed { get; }           // ¹¥»÷¼ä¸ô
    protected float mCurrentSecond = 0;                 // µ±Ç°¹¥»÷¼ä¸ô

    protected virtual void Awake() { }

    protected virtual void Start() { }

    public virtual void InitID(int id)
    {
        this.ID = id;
    }

    protected virtual void Update()
    {
        mCurrentSecond += Time.deltaTime;

        if(mCurrentSecond > attackSpeed)
        {
            mCurrentSecond = 0;
            Attack();
        }
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