using System;
using System.Collections.Generic;

[Serializable]
public class BuffList
{
    // Ƶ������ɾ�� O(1)���Ӷ� ����Ҫ�������Ԫ��ֻ��Ҫ˳�����Ԫ��
    public LinkedList<BaseBuff> buffs = new LinkedList<BaseBuff>();

    public void AddBuff(int id)
    {
        var buff = BuffFactory.GetBuffByID(id);
        var node = Find(buff);
        if (node == null)
        {
            // û���ҵ�ֱ����ĩβ���
            buffs.AddLast(buff);
            buff.OnAdd();
        }
        else
        {
            if (node.Value.ID == buff.ID)
            {
                // ͬһ��buff�ٴ����
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
                // �ҵ�ֱ���Ƴ�һ������
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