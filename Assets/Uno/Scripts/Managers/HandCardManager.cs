using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Splines;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;

using System;

using DG.Tweening;
using System.Linq.Expressions;

/// <summary>
/// Visual Manager
/// </summary>
public class HandCardManager : MonoBehaviour

{


    readonly List<Card> handCard = new List<Card>();
    Card selected;
    SplineContainer handSpline;
    [SerializeField] float initalSpace = 1f;
    int PlayerCode = 0;
    GameObject cardEntityParent;
    GameObject cardVisualParent;



    // Start is called before the first frame update
    void Start()
    {
        handSpline = transform.Find("HandSpline").GetComponent<SplineContainer>();
        cardEntityParent = transform.Find("CardEntityParent").gameObject;
        cardVisualParent = transform.Find("CardVisualParent").gameObject;
        
        AddListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddListeners()
    {
        
    }

    //
    private void InsertHandCard(IEventMessage message)
    {
        var msg = message as PlayerPlusCardEvent;

        if (msg != null)
        {
            if (PlayerCode == msg.playerCode)
            {
                Card card = msg.card;
                handCard.Add(card);
                card.transform.parent = handSpline.transform;

                Debug.Log("Card Received!");

                UpdateCardTransform();
            }
        }
    }

    void OnPlayerRemoveCardEvent(IEventMessage message)
    {
        var msg = message as PlayerRemoveCardEvent;

        if (msg.Code != PlayerCode) return;

        foreach (var item in handCard)
        {
            if (item.cardInfo.Campare(msg.Card))
            {
                
            }
        }
        
    }

    //temp function: update all 
    void UpdateCardTransform()
    {
        if (handCard.Count == 0) return;

        int count = handCard.Count;

        for (int i = 0; i < count; i++)
        {
            float space = (i + 0.5f) / count;
            Vector3 pos = handSpline.EvaluatePosition(space);
            Vector3 rot = handSpline.EvaluateUpVector(space);
            handCard[i].SetTransform(pos);
            handCard[i].SetRotation(rot);
        }
    }
    
    public void SwapCardIndex(int index1, int index2)
    {
        if (index1 < 0 || index1 >= handCard.Count || index2 < 0 || index2 >= handCard.Count) return;
        
        (handCard[index1], handCard[index2]) = (handCard[index2], handCard[index1]);
        
        UpdateCardTransform();
    }
    
    void TryPlayCard(CardInfo info)
    {
        
        
    }
    
    
}
