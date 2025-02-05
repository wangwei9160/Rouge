using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public virtual int ID {  get; private set; }

    public GameObject player;

    private int status;
    public int Status
    {
        get { return status; }
        set { status = value; }
    }

    private Animator animator;
    private string currentAnimation = "";

    protected EnemyTplInfo Info
    {
        get
        {
            return TplUtil.GetEnemyTplDic()[ID];
        }
    }
    public EnemyAttribute attr;

    public float maxHp;
    public float Hp;
    public float curHp
    {
        get
        {
            return Hp;
        }
        set
        {
            Hp = value;
        }
    }

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.GameOver , GameOver);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.GameOver, GameOver);
    }

    public void GameOver()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").gameObject;
        Init();
    }

    void Update()
    {
        StatusChange();
        Move();
    }

    public void Init()
    {
        attr = new EnemyAttribute();
        float upRate = (float)Math.Exp(0.1 * (GameManager.Instance.gameData.CurrentWave - 1));
        float upRateLow = (float)Math.Exp(0.01 * (GameManager.Instance.gameData.CurrentWave - 1));
        attr.攻击力 = Info.Attack * upRate;
        attr.防御 = Info.Defend * upRate;
        attr.经验值 = Info.Exp;
        attr.移动速度 = Math.Min(Info.Speed / 100 * upRateLow , 2f);
        maxHp = Info.Hp + 10 * (GameManager.Instance.gameData.CurrentWave - 1) + 20 * (GameManager.Instance.gameData.curLevel - 1);
        curHp = maxHp;
        //Debug.Log(string.Format("ID {0} Hp {1} Exp {2} Speed {3}", ID, curHp, attr.经验值 , attr.移动速度));
    }

    public void Die()
    {
        status = 5;
        ChangeAnimation("Death");
        //Debug.Log("Go Death");
        EventCenter.Broadcast(EventDefine.OneEnemyDeath, gameObject);
    }

    protected virtual void StatusChange() { }

    protected virtual void Move() { }

    public bool Damage(float damge)
    {
        if (curHp <= 0f)
        {
            return false;
        }
        curHp -= damge;
        return true;
    }

    public bool ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if(currentAnimation == "Death")
        {
            return false;
        }
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossfade);
            return true;
        }
        return false;
    }

    public IEnumerator AttackFunc()
    {
        yield return new WaitForSeconds(1.0f);
        DamageInfo info = new DamageInfo(0, attr.攻击力, 0, 100f);
        foreach (var buff in GameManager.Instance.gameData.playerBuffList.buffs)
        {
            buff.OnBeforeHurt(ref info);
        }
        GameManager.Instance.OnHpChange((int)info.Value);
        DamageUIManager.Instance.ShowDamgeText(player.transform, info);
        yield return new WaitForSeconds(0.5f);
        ChangeAnimation("Idle");
    }

}
