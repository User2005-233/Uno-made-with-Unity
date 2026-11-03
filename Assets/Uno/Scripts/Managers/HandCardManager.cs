using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Splines;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;

using System;

using DG.Tweening;
using System.Linq;
using Unity.VisualScripting;

//card visual manager
public class HandCardManager : MonoBehaviour
{
    List<Card> handCard = new List<Card>();
    List<Vector3> cardPositions = new List<Vector3>();
    List<Vector3> cardRotations = new List<Vector3>();
    public Card selected;
    SplineContainer handSpline;
    [SerializeField] float initalSpace = 1f;
    int PlayerCode = 0;

    // TODO: add ai play card module

    void Awake()
    {
        handSpline = transform.Find("HandSpline").GetComponent<SplineContainer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        AddListeners();
    }

    // Update is called once per frame
    void Update()
    {
        //iterate the full hand card to see if card position need to be swaped
        
    }
    

    void AddListeners()
    {
    }

    public void BroadCastPlayCard(CardInfo cardInfo)
    {
        EventManager.Instance.SendMessage(new CardPlayEvent(PlayerCode, cardInfo));
    }


    public void InsertHandCard(Card card)
    {
        handCard.Add(card);
        card.transform.SetParent(handSpline.transform, false);
        card.manager = this;
        card.SetContentVisible(card.inTruePlayerHand);
        Debug.Log("Card Received!");
        UpdateCardTransform();
        SortCardsAndPositions(handCard);
    }
    
    public void RemoveHandCard(Card card)
    {
        handCard.Remove(card);
        UpdateCardTransform();
        SortCardsAndPositions(handCard);
    }


    //temp function: update all transform settings
    //give each card a position and rotation based on the spline, and they go to their own position with a smooth animation
    void UpdateCardTransform()
    {
        if (handCard.Count == 0) return;

        int count = handCard.Count;
        cardPositions.Clear();
        cardRotations.Clear();

        for (int i = 0; i < count; i++)
        {
            float space = (i + 0.5f) / count;
            Vector3 pos = handSpline.EvaluatePosition(space);
            Vector3 rot = handSpline.EvaluateUpVector(space);
            cardPositions.Add(pos);
            cardRotations.Add(rot);
        }
    }
    

    public void SortCardsAndPositions(List<Card> cardList)
    {
        if (cardList == null || cardList.Count == 0) return;

        // 2. 使用 LINQ 进行多级排序（这里以 先按颜色、再按类型、最后按数字 排序为例）
        List<Card> sortedList = cardList
            .OrderBy(c => GetColorOrder(c.cardInfo.color))
            .ThenBy(c => GetTypeOrder(c.cardInfo.type))
            .ThenBy(c => c.cardInfo.number)
            .ToList();

        // 3. 将物理坐标重新赋给排序后的卡牌对象
        for (int i = 0; i < sortedList.Count; i++)
        {
            sortedList[i].SetTransform(cardPositions[i]);
            sortedList[i].SetRotation(cardRotations[i]);
        }

        // 4. 将原本的 List 内容更新为排序后的结果
        cardList.Clear();
        cardList.AddRange(sortedList);
    }
    
    private static int GetColorOrder(CardColor color)
    {
        return color switch
        {
            CardColor.red => 1,
            CardColor.yellow => 2,
            CardColor.green => 3,
            CardColor.blue => 4,
            _ => 0 // 万能牌最后
        };
    }

    private static int GetTypeOrder(CardType type)
    {
        return type switch
        {
            CardType.Number => 0,
            CardType.Skip => 1,
            CardType.Reverse => 2,
            CardType.PlusTwo => 3,
            CardType.Wild => 4,
            CardType.WildDrawFour => 5,
            _ => 6
        };
    }
    
    
}
