using System.Collections.Generic;

using UnityEngine;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;

using Addressables = UnityEngine.AddressableAssets.Addressables;

/// <summary>
/// 卡牌对象池管理器
/// 用于管理和复用卡牌对象，提高游戏性能
/// </summary>
public class CardPoolManager : MonoBehaviour

{
    [Header("卡牌对象池设置")]

    [SerializeField] private string cardPrefabAddress = "Assets/Uno/Prefabs/Card.prefab";

    [SerializeField] private int initialPoolSize = 20; // 初始池大小
    [SerializeField] private int maxPoolSize = 200; // 最大池大小

    [SerializeField] private Transform poolParent; // 对象池父对象
    private Queue<Card> availableCards = new Queue<Card>(); // 可用卡牌队列
    private List<Card> activeCards = new List<Card>(); // 活跃卡牌列表
    private GameObject cardPrefab; // 卡牌预制体    
    private static CardPoolManager instance;

    public static CardPoolManager Instance
    {
        get
        {
            if (instance == null)

            {
                GameObject go = new GameObject("CardPoolManager");

                instance = go.AddComponent<CardPoolManager>();

                DontDestroyOnLoad(go);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

            InitializePool();
        }
        else if (instance != this)

        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初始化对象池
    /// </summary>
    private void InitializePool()
    {
        if (poolParent == null)

        {
            GameObject poolParentObj = new GameObject("CardPool");

            poolParentObj.transform.SetParent(transform);

            poolParent = poolParentObj.transform;
        }

        // 加载卡牌预制体        
        LoadCardPrefab();
    }

    /// <summary>
    /// 加载卡牌预制体
    /// </summary>
    private void LoadCardPrefab()
    {
        Addressables.LoadAssetAsync<GameObject>(cardPrefabAddress).Completed += OnPrefabLoaded;
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardPrefab = handle.Result;

            // 预热对象池
            PrewarmPool();
        }

        else
        {
            Debug.LogError($"Failed to load card prefab: {cardPrefabAddress}");
        }
    }

    /// <summary>
    /// 创建新的卡牌对象
    /// </summary>
    private void PrewarmPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewCard();
        }
    }

    /// <summary>
    /// 创建新的卡牌对象
    /// </summary>
    private Card CreateNewCard()
    {
        if (cardPrefab == null)
        {
            Debug.LogWarning("Card prefab not loaded yet!");

            return null;
        }

        GameObject cardObj = Instantiate(cardPrefab, poolParent);

        cardObj.SetActive(false);

        Card card = cardObj.GetComponent<Card>();

        if (card == null)
        {
            Debug.LogError("Card component not found on prefab!");

            Destroy(cardObj);

            return null;
        }

        availableCards.Enqueue(card);

        return card;
    }

    /// <summary>
    /// 获取卡牌对象
    /// </summary>

    /// <param name="parent">父对象变换</param>

    /// <returns>卡牌对象</returns>
    public Card GetCard(Transform parent = null)
    {
        Card card = null;

        // 从可用队列中获取卡牌
        if (availableCards.Count > 0)
        {
            card = availableCards.Dequeue();
        }

        else
        {
            // 如果池未满，创建新卡牌
            if (activeCards.Count < maxPoolSize)
            {
                card = CreateNewCard();
            }

            else
            {
                Debug.LogWarning("Card pool is full! Cannot create more cards.");

                return null;
            }
        }

        if (card != null)
        {
            // 设置父对象
            if (parent != null)
            {
                card.transform.SetParent(parent);
            }

            else
            {
                card.transform.SetParent(null);
            }

            // 激活对象
            card.gameObject.SetActive(true);

            // 添加到活跃列表
            activeCards.Add(card);

            // 重置卡牌状态
            ResetCard(card);
        }

        return card;
    }

    /// <summary>
    /// 返回卡牌到对象池
    /// </summary>

    /// <param name="card">要返回的卡牌对象</param>
    public void ReturnCard(Card card)
    {
        if (card == null) return;

        // 从活跃列表中移除
        if (activeCards.Contains(card))
        {
            activeCards.Remove(card);
        }

        // 重置卡牌状态
        ResetCard(card);

        // 设置父对象为池对象
        card.transform.SetParent(poolParent);

        card.gameObject.SetActive(false);

        // 添加到可用队列
        availableCards.Enqueue(card);
    }

    /// <summary>
    /// 返回多个卡牌到对象池
    /// </summary>

    /// <param name="cards">要返回的卡牌对象列表</param>
    public void ReturnCards(List<Card> cards)
    {
        if (cards == null) return;

        foreach (Card card in cards)
        {
            ReturnCard(card);
        }
    }

    /// <summary>
    /// 重置卡牌状态
    /// </summary>
    private void ResetCard(Card card)
    {
        if (card == null) return;

        // 重置变换属性        
        card.transform.localPosition = Vector3.zero;

        card.transform.localRotation = Quaternion.identity;

        // 重置卡牌颜色
        card.ColorReset();
    }

    /// <summary>
    /// 清空对象池
    /// </summary>
    public void ClearPool()
    {
        // 返回所有活跃的卡牌
        List<Card> cardsToReturn = new List<Card>(activeCards);
        ReturnCards(cardsToReturn);

        // 销毁所有可用的卡牌

        {
            Card card = availableCards.Dequeue();

            if (card != null)

            {
                Destroy(card.gameObject);
            }
        }

        activeCards.Clear();
    }

    /// <summary>
    /// 获取对象池统计信息
    /// </summary>
    public PoolStats GetPoolStats()
    {
        return new PoolStats

        {
            availableCount = availableCards.Count,
            activeCount = activeCards.Count,
            totalCount = availableCards.Count + activeCards.Count,
            maxSize = maxPoolSize

        };
    }

    /// <summary>
    /// 对象池统计信息结构体
    /// </summary>
    public struct PoolStats

    {
        public int availableCount;

        public int activeCount;

        public int totalCount;

        public int maxSize;
    }

    private void OnDestroy()
    {
        ClearPool();
    }
}
