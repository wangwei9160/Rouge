using System;
using System.Collections.Generic;

[Serializable]
public class BuffList
{
    // 频繁插入删除 O(1)复杂度 不需要随机访问元素只需要顺序访问元素
    public LinkedList<BaseBuff> buffs = new LinkedList<BaseBuff>();

    public void AddBuff(int id)
    {
        var buff = BuffFactory.GetBuffByID(id);
        var node = Find(buff);
        if (node == null)
        {
            // 没有找到直接在末尾添加
            buffs.AddLast(buff);
            buff.OnAdd();
        }
        else
        {
            if (node.Value.ID == buff.ID)
            {
                // 同一个buff再次添加
                node.Value.Cnt++;
            }else
            {
                buffs.AddBefore(node , buff);
                buff.OnAdd();
            }
        }
    }

    public bool RemoveBuff(int id)
    {
        foreach (var buff in buffs)
        {
            if(buff.ID == id)
            {
                // 找到直接移除一个计数
                buff.Cnt--;
                if(buff.Cnt == 0)
                {
                    buffs.Remove(buff);
                    buff.OnRemove();
                }
                return true;
            }
        }
        return false;
    }

    private LinkedListNode<BaseBuff> Find(BaseBuff buff)
    {
        LinkedListNode<BaseBuff> node = buffs.First;
        while (node != null)
        {
            if(node.Value.ID == buff.ID)
            {
                return node;
            }
            node = node.Next;
        }
        return node;
    }

}