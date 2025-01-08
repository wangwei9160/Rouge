using UnityEngine;

public class BaseItem 
{
    public virtual int ID { get; set; }

    public virtual string Name { get; set; }

    public virtual void OnGet()
    {
        Debug.Log("��õ��� : " + Name);
    }

    public virtual void OnDiscard()
    {
        Debug.Log("�������� : " + Name);
    }
}