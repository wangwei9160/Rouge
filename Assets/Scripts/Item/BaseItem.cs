using UnityEngine;

public class ItemInfo
{
    public string Name { get; set; }
}

public class BaseItem 
{
    public virtual int ID { get; set; }

    public ItemInfo Info { get; }

    public virtual void OnGet()
    {
        Debug.Log("��õ��� : " + Info.Name);
    }

    public virtual void OnDiscard()
    {
        Debug.Log("�������� : " + Info.Name);
    }
}