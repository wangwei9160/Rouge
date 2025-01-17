using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventCenter
{
    private static Dictionary<EventDefine , Delegate> m_Event = new Dictionary<EventDefine, Delegate>();

    //public static Dictionary<EventDefine, Action> m_EventAction = new Dictionary<EventDefine, Action>();

    private static void OnListenerAdding(EventDefine eventType , Delegate del)
    {
        if(!m_Event.ContainsKey(eventType))
        {
            m_Event.Add(eventType, null);
        }
        Delegate d = m_Event[eventType];
        if(d != null && d.GetType() != del.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}[添加]不同类型的委托，{1}和{2}",eventType , d.GetType() , del.GetType()));
        }
    }

    private static void OnListenerRemoving(EventDefine eventType, Delegate del)
    {
        if(m_Event.ContainsKey(eventType))
        {
            Delegate d = m_Event[eventType];
            if(d == null)
            {
                throw new Exception(string.Format("事件{0}没有正在监听的委托", eventType));
            }else if(d.GetType() != del.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}[移除]不同类型的委托，{1}和{2}", eventType, d.GetType(), del.GetType()));
            }
        }else
        {
            throw new Exception(string.Format("不存在事件{0}", eventType));
        }
    }

    private static void OnEventRemove(EventDefine eventType)
    {
        if (m_Event[eventType] == null)
        {
            m_Event.Remove(eventType);
        }
    }


    public static void AddListener(EventDefine eventType , CallBack callback)
    {
        OnListenerAdding(eventType , callback);
        m_Event[eventType] = (CallBack)m_Event[eventType] + callback;
    }

    public static void AddListener<T>(EventDefine eventType, CallBack<T> callback)
    {
        OnListenerAdding(eventType, callback);
        m_Event[eventType] = (CallBack<T>)m_Event[eventType] + callback;
    }


    public static void RemoveListener(EventDefine eventType, CallBack callback)
    {
        OnListenerRemoving(eventType, callback);
        m_Event[eventType] = (CallBack)m_Event[eventType] - callback;
        OnEventRemove(eventType);
    }

    public static void RemoveListener<T>(EventDefine eventType, CallBack<T> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_Event[eventType] = (CallBack<T>)m_Event[eventType] - callback;
        OnEventRemove(eventType);
    }

    public static void Broadcast(EventDefine eventType)
    {
        Delegate d;
        if(m_Event.TryGetValue(eventType, out d))
        {
            CallBack callBack = (CallBack)d;
            if(callBack != null)
            {
                callBack();
            }else 
            {
                throw new Exception(string.Format("广播事件{0}错误",eventType));
            }
        }
    }

    public static void Broadcast<T>(EventDefine eventType , T arg)
    {
        Delegate d;
        if (m_Event.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = (CallBack<T>)d;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件{0}错误", eventType));
            }
        }
    }

}