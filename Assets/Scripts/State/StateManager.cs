using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{
    private IDictionary<Type, object> services;

    private static StateManager _instance;

    public static StateManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void LoadServices() {
        StateManager.Instance.RegisterService<EventService>(new EventService());
        StateManager.Instance.RegisterService<PlayerService>(new PlayerService());
	}


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        services = new Dictionary<Type, object>();
        DontDestroyOnLoad(this);
        LoadServices();

    }


    public void RegisterService<T>(T service)
    {
        services[typeof(T)] = service;
    }

    public T GetService<T>()
    {
        try
        {
            return (T)services[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            throw new ApplicationException($"The requested service {typeof(T)} is not registered.");
        }
    }
}
