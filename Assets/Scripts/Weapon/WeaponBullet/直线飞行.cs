using UnityEngine;

public class ֱ�߷��� : MonoBehaviour
{
    public Vector3 dir;                 // Ĭ�Ϸ��з���

    public float moveSpeed = 10f;       // Ĭ�Ϸ����ٶ�

    protected virtual void Start()
    {
        Destroy(gameObject, 5);         // Ĭ�ϴ�������
    }

    protected virtual void Update()
    {
        Move();
    }

    public void ResetY2Empty()
    {
        dir.y = 0;
    }

    public void Move()
    {
        if (gameObject.transform.position.z <= -15 || gameObject.transform.position.x <= -11 || gameObject.transform.position.x >= 11)
        {
            Destroy(gameObject);
        }
        //Debug.Log(string.Format("{0}",dir.ToString()));
        gameObject.transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void SetMoveToTarget(Transform player, Transform target)
    {
        gameObject.transform.position = player.position;
        // ����Ŀ���ƶ�
        dir = (target.position - player.position).normalized;
        ResetY2Empty();
        var rota = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = rota;
    }

    public void SetMove(Vector3 player , Vector3 target)
    {
        gameObject.transform.position = player;
        dir = (target - player).normalized;
        ResetY2Empty();
        var rota = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = rota;
    }

    public void SetMoveDirectionAndRotation(Vector3 dr , Quaternion rota)
    {
        this.dir = dr;
        ResetY2Empty();
        gameObject.transform.rotation = rota;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(string.Format("�Ӵ�������{0}", other.name));
        }
    }

}
