using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour 
{

    // ��ʶ������ID�����ڳ�ʼ������
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

    protected virtual float attackRange { get; }        // ������Χ
    
    public virtual float attack { get; }                 // ������
    public virtual float attackSpeed { get; }           // �������
    protected float mCurrentSecond = 0;                 // ��ǰ�������

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