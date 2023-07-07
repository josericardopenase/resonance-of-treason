using System;
using System.Collections.Generic;

public class EventService
{
    private Dictionary<string, Action<object>> eventDictionary;

    public EventService()
    {
        eventDictionary = new Dictionary<string, Action<object>>();
    }

    public void Subscribe(string eventName, Action<object> callback)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = callback;
        }
        else
        {
            eventDictionary[eventName] += callback;
        }
    }

    public void Subscribe<T>(T eventName, Action<object> callback) where T : Enum
    {
        Subscribe(EventHelper.GetFullName(eventName), callback);
    }


    public void Unsubscribe(string eventName, Action<object> callback)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= callback;
        }
    }
    public void Trigger<T>(T eventName, object metadata) where T : Enum
    {
        Trigger(EventHelper.GetFullName(eventName), metadata);
    }

    public void Trigger(string eventName, object metadata)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke(metadata);
        }
    }
}
