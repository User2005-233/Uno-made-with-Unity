using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Splines;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;

using System;

using DG.Tweening;
using System.Linq;

/// <summary>
/// Visual Manager
/// </summary>
public class HandCardManager : MonoBehaviour
{
    List<Card> handCard = new List<Card>();
    List<Vector3> cardPositions = new List<Vector3>();
    List<Vector3> cardRotations = new List<Vector3>();
    Card selected;
    SplineContainer handSpline;
    [SerializeField] float initalSpace = 1f;
    int PlayerCode = 0;

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



    public void InsertHandCard(Card card)
    {
        handCard.Add(card);
        card.transform.parent = handSpline.transform;
        Debug.Log("Card Received!");
    }
    
    public void RemoveHandCard(Card card)
    {
        handCard.Remove(card);
        UpdateCardTransform();
    }


    //temp function: update all transform settings
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
        if (cardList == null || cardList.Count <= 1) return;

        // 2. 使用 LINQ 进行多级排序（这里以 先按颜色、再按类型、最后按数字 排序为例）
        List<Card> sortedList = cardList
            .OrderBy(c => c.cardInfo.color)
            .ThenBy(c => c.cardInfo.type)
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
    
    
}
