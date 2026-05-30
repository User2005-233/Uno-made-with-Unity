using UnityEngine;

public abstract class ModuleSingleton<T> where T : class,new()
{
    private static T _instance;

    public static T Instance

    {
        get

        {
            if (_instance == null)
                _instance = new T();

            return _instance;
        }
    }

    protected ModuleSingleton()
    {
        if (_instance != null)
            throw new System.Exception($"{typeof(T)} instance already created.");
        _instance = this as T;
    }

    protected void DestroySingleton()
    {
        _instance = null;
    }
}
