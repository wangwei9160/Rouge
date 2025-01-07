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
        Debug.Log("获得道具 : " + Info.Name);
    }

    public virtual void OnDiscard()
    {
        Debug.Log("丢弃道具 : " + Info.Name);
    }
}