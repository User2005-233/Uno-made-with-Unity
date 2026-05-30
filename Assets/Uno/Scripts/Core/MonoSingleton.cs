using UnityEngine;

/// <summary>
/// MonoBehaviour 单例模式基类
/// 使用示例：public class YourManager : MonoSingleton<YourManager> { }
/// </summary>

/// <typeparam name="T">单例类型</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>

{
    private static T _instance;

    private static bool _isApplicationQuitting = false;

    private static readonly object _lock = new object();
    /// <summary>
    /// 是否持久化实例
    /// </summary>
    protected virtual bool IsPersistent => true;

    /// <summary>
    /// 是否自动创建实例
    /// </summary>
    protected virtual bool AutoCreate => true;

    /// <summary>
    /// 
    /// </summary>
    public static T Instance

    {
        get

        {
            //  null
            if (_isApplicationQuitting)

            {
                Debug.LogWarning($"[MonoSingleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null;
            }

            lock (_lock)

            {
                if (_instance == null)

                {
                    // 查找现有实例
                    _instance = FindFirstObjectByType<T>();

                    // 如果没找到
                    if (_instance == null && Application.isPlaying)

                    {
                        GameObject singleton = new GameObject($"[{typeof(T).Name}]");
                        _instance = singleton.AddComponent<T>();

                        Debug.Log($"[MonoSingleton] An instance of {typeof(T)} was auto-created.");
                    }
                }

                return _instance;
            }
        }
    }

    /// <summary>
    /// 检查实例是否存在
    /// </summary>
    public static bool HasInstance => _instance != null && !_isApplicationQuitting;

    protected virtual void Awake()
    {
        lock (_lock)

        {
            if (_instance == null)

            {
                _instance = this as T;

                // 设置为 DontDestroyOnLoad
                if (IsPersistent && Application.isPlaying)

                {
                    DontDestroyOnLoad(gameObject);
                }

                OnSingletonAwake();
            }

            else if (_instance != this)

            {
                // 销毁重复实例
                Debug.LogWarning($"[MonoSingleton] Duplicate instance of {typeof(T)} found. Destroying duplicate.");
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        lock (_lock)

        {
            if (_instance == this)

            {
                _instance = null;

                OnSingletonDestroy();
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }

    /// <summary>
    /// 在 Awake 时调用
    /// </summary>
    protected virtual void OnSingletonAwake() { }
    /// <summary>
    /// 在 OnDestroy 时调用
    /// </summary>
    protected virtual void OnSingletonDestroy() { }
    /// <summary>
    /// 销毁实例
    /// </summary>
    public static void DestroyInstance()
    {
        if (HasInstance)

        {
            Destroy(_instance.gameObject);

            _instance = null;
        }
    }
}
