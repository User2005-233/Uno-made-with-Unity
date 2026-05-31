using System;

using System.Collections.Generic;

/// <summary>
/// 事件管理器
/// </summary>
public class EventManager : ModuleSingleton<EventManager>

{
    private readonly Dictionary<int, LinkedList<Action>> _actionListeners = new Dictionary<int, LinkedList<Action>>(100);
    private readonly Dictionary<int, LinkedList<Action<IEventMessage>>> _eventListeners = new Dictionary<int, LinkedList<Action<IEventMessage>>>(100);
    private readonly Dictionary<int, LinkedList<Action<INetEventMessage>>> _netEventListeners = new Dictionary<int, LinkedList<Action<INetEventMessage>>>(100);
    // 添加事件监听器
    public void AddListener<TEvent>(System.Action<IEventMessage> listener) where TEvent : IEventMessage
    {
        AddListener(typeof(TEvent), listener);
    }

    public void AddListener(System.Type eventType, System.Action<IEventMessage> listener)
    {
        int eventId=eventType.GetHashCode();
        AddListener(eventId, listener);
    }

    public void AddListener(int eventId, System.Action<IEventMessage> listener)
    {
        if (_eventListeners.ContainsKey(eventId) == false)
            _eventListeners.Add(eventId, new LinkedList<Action<IEventMessage>>());
        if (_eventListeners[eventId].Contains(listener)==false)
            _eventListeners[eventId].AddLast(listener);
    }

    // 移除事件监听器
    public void RemoveListener<TEvent>(System.Action<IEventMessage> listener)where TEvent : IEventMessage
    {
        RemoveListener(typeof(TEvent), listener);
    }

    public void RemoveListener(System.Type eventType, System.Action<IEventMessage> listener)
    {
        int eventId=eventType.GetHashCode();
        RemoveListener(eventId, listener);
    }

    public void RemoveListener(int eventId, System.Action<IEventMessage> listener)
    {
        if (_eventListeners.ContainsKey(eventId))

        {
            if (_eventListeners[eventId].Contains(listener))

            {
                _eventListeners[eventId].Remove(listener);
            }
        }
    }

    // 发送事件消息    
    public void SendMessage(IEventMessage message)
    {
        int eventId=message.GetType().GetHashCode();
        SendMessage(eventId, message);
    }

    public void SendMessage(int eventId, IEventMessage message)
    {
        if (_eventListeners.ContainsKey(eventId)==false) return;

        LinkedList<Action<IEventMessage>> listeners = _eventListeners[eventId];

        if (listeners.Count > 0)

        {
            var currentNode = listeners.Last;

            while (currentNode != null)

            {
                currentNode.Value.Invoke(message);

                currentNode = currentNode.Previous;
            }
        }
    }

    // 添加网络事件监听器
    public void AddNetListener<TEvent>(Action<INetEventMessage> listener) where TEvent : INetEventMessage
    {
        Type type = typeof(TEvent);

        AddNetListener(type, listener);
    }

    void AddNetListener(Type eventType,Action<INetEventMessage> listener)
    {
        int eventId = eventType.GetHashCode();
        AddNetListener(eventId, listener);
    }

    void AddNetListener(int eventId, Action<INetEventMessage> listener)
    {
        if (_netEventListeners.ContainsKey(eventId) == false)
            _netEventListeners.Add(eventId, new LinkedList<Action<INetEventMessage>>());
        if (_netEventListeners[eventId].Contains(listener) == false)
            _netEventListeners[eventId].AddLast(listener);
    }

    // 移除网络事件监听器
    public void RemoveNetListener<TEvent>(System.Action<INetEventMessage> listener) where TEvent : INetEventMessage
    {
        RemoveNetListener(typeof(TEvent), listener);
    }

    public void RemoveNetListener(System.Type eventType, System.Action<INetEventMessage> listener)
    {
        int eventId = eventType.GetHashCode();
        RemoveNetListener(eventId, listener);
    }

    public void RemoveNetListener(int eventId, System.Action<INetEventMessage> listener)
    {
        if (_netEventListeners.ContainsKey(eventId))

        {
            if (_netEventListeners[eventId].Contains(listener))

            {
                _netEventListeners[eventId].Remove(listener);
            }
        }
    }

    // 发送网络事件消息    
    public void SendNetMessage(INetEventMessage message)
    {
        int eventId = message.GetType().GetHashCode();
        SendNetMessage(eventId, message);
    }

    public void SendNetMessage(int eventId, INetEventMessage message)
    {
        if (_netEventListeners.ContainsKey(eventId) == false) return;

        LinkedList<Action<INetEventMessage>> listeners = _netEventListeners[eventId];

        if (listeners.Count > 0)

        {
            var currentNode = listeners.Last;

            while (currentNode != null)

            {
                currentNode.Value.Invoke(message);

                currentNode = currentNode.Previous;
            }
        }
    }

    // 清空所有监听器
    public void ClearListeners()
    {
        foreach (int eventId in _eventListeners.Keys)
        {
            _eventListeners[eventId].Clear();
        }

        _eventListeners.Clear();
    }

    // 添加动作事件监听器
    public void AddActionListener<TEvent>(System.Action listener) where TEvent : IAction
    {
        AddActionListener(typeof(TEvent), listener);
    }

    public void AddActionListener(System.Type eventType, System.Action listener)
    {
        int eventId = eventType.GetHashCode();
        AddActionListener(eventId, listener);
    }

    public void AddActionListener(int eventId, System.Action listener)
    {
        if (_actionListeners.ContainsKey(eventId) == false)
            _actionListeners.Add(eventId, new LinkedList<Action>());
        if (_actionListeners[eventId].Contains(listener) == false)
            _actionListeners[eventId].AddLast(listener);
    }

    // 触发事件
    public void TriggerEvent<TEvent>()where TEvent : IAction
    {
        int eventId= typeof(TEvent).GetHashCode();
        TriggerEvent(eventId);
    }

    void TriggerEvent(int eventId)
    {
        if (_actionListeners.ContainsKey(eventId) == false) return;

        LinkedList<Action> listeners = _actionListeners[eventId];

        if (listeners.Count > 0)

        {
            var currentNode = listeners.Last;

            while (currentNode != null)

            {
                currentNode.Value.Invoke();

                currentNode = currentNode.Previous;
            }
        }
    }
}
