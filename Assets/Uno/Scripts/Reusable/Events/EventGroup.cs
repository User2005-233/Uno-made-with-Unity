using System;

using System.Collections;

using System.Collections.Generic;

public class EventGroup

{
    private readonly Dictionary<System.Type, List<Action<IEventMessage>>> _cachedListener = new Dictionary<Type, List<Action<IEventMessage>>>();
    public void AddListener<TEvent>(System.Action<IEventMessage> listener)where TEvent : IEventMessage
    {
    }
}
