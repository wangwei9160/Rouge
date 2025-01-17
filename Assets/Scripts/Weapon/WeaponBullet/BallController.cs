using UnityEngine;

public class BallController : ֱ�߷���
{
    public float damage = 10f;          // �˺�
    

    public string Name;                 // ��Դ����������

    public void ResetInfo(float dam , string name)
    {
        damage = dam;
        Name = name;
    }

    public bool isAlive = true;         // �Ƿ���Դ�͸

    public void SetMoveToTarget(GameObject player , GameObject target)
    {
        gameObject.transform.position = player.transform.position;
        // ����Ŀ���ƶ�
        dir = (target.transform.position - gameObject.transform.position).normalized;
        ResetY2Empty();
        var rota = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = rota;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy") && isAlive)
        {
            //bool ok = other.gameObject.GetComponent<MonsterController>().Damge(damage);
            bool ok = other.gameObject.GetComponent<BaseEnemy>().Damage(damage);
            if (!ok) return;
            isAlive = false;
            Destroy(gameObject);
            DamageUIManager.Instance.ShowDamgeText(other.gameObject.transform,(int)damage);
            UIManager.Instance.UpdateWeaponDamage(Name, (int)damage);
        }
    }

}
