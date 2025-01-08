using UnityEngine;

public class BaseItem 
{
    public virtual int ID { get; set; }

    public virtual string Name { get; set; }

    public virtual void OnGet()
    {
        Debug.Log("获得道具 : " + Name);
    }

    public virtual void OnDiscard()
    {
        Debug.Log("丢弃道具 : " + Name);
    }
}