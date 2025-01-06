using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour 
{
    public int ID = -1;

    protected WeaponType type = WeaponType.Unknown;

    protected float attackRange;        // ������Χ
    
    public virtual float attck { get; } // ������
    public float attackSpeed;           // �����ٶ�
    protected float mCurrentSecond = 0; // ��ǰ�������

    protected virtual void Awake() { }

    protected virtual void Start() { }

    public virtual void Init(int id)
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