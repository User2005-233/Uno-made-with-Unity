using System.Collections;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Text;

using Unity.VisualScripting;

using UnityEngine;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;


using DG.Tweening;


public class Card : MonoBehaviour

{
    public CardType cardType;
    public CardColor cardColor;
    public int cardNum = -1;
    public CardInfo cardInfo;
    bool inHand = false;
    bool onDrag = false;
    SpriteRenderer spriteRenderer;
    readonly Color hovered = new Color(0.75f, 0.75f, 0.75f);
    readonly string unoAllSprite = "UNO_ALL.png";
    readonly StringBuilder sb = new StringBuilder();
    Sprite cardSprite = null;
    public GameObject cardVisualPrefab;
    Vector3 position = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Vector3 currentVelocity;
    public float smoothingTime = 0.2f;
    private float zCoord;
    public HandCardManager manager;

    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        if (cardSprite != null)
        {
            spriteRenderer.sprite = cardSprite;
        }
    }

    #region Card Movement
    //TODO: implement smooth follow the mouse movement if draged
    void Update()
    {
        if (inHand)
        {
            if (onDrag) OnDrag();
            else OnDragEnd();
        }
    }

    void OnMouseDown()
    {
        onDrag = true;
    }
    void OnMouseUp()
    {
        onDrag = false;
    }
    void OnMouseOver()
    {
        spriteRenderer.color = hovered;
    }
    void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
    
    
    void OnDrag()
    {
        Vector3 mousePosition = GetMouseWorldPos();
        mousePosition.z = zCoord;

        transform.position = Vector3.SmoothDamp(
                    transform.position,
                    mousePosition,
                    ref currentVelocity,
                    smoothingTime,
                    Mathf.Infinity,
                    Time.deltaTime
                );

        //check if reached target
        if (Vector3.Distance(transform.position, mousePosition) < 0.01f)
        {
            transform.position = mousePosition;
        }
    }
    void OnDragEnd()
    {
        transform.position = Vector3.SmoothDamp(
                    transform.position,
                    position,
                    ref currentVelocity,
                    smoothingTime,
                    Mathf.Infinity,
                    Time.deltaTime
                );
        if (Vector3.Distance(transform.position, position) < 0.01f)
        {
            transform.position = position;
        }
    }
    //TODO: implement tilt when dragged, receive velocity from mouse movement
    void LagRotation()
    {
        
    }

    private Vector3 GetMouseWorldPos()
    {
        // Pixel coordinates of mouse (x, y)
        Vector3 mousePoint = Input.mousePosition;

        // Depth coordinate (z) of the object
        mousePoint.z = zCoord;

        // Convert to world space
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    #endregion

    #region Sprite Code
    public void ColorReset()
    {
        spriteRenderer.color = Color.white;
    }

    //multiple set sprite functions
    public void SetProperties(WildFunc func)
    {
        string temp = null;

        sb.Append(unoAllSprite);

        switch (func)
        {
            case WildFunc.PlusFour:
                cardType = CardType.WildDrawFour;

                temp = "[UNO_ALL_54]";

                break;

            case WildFunc.Wild:
                cardType = CardType.Wild;

                temp = "[UNO_ALL_52]";

                break;

            default:
                break;
        }

        sb.Append(temp);

        string path = sb.ToString();
        Addressables.LoadAssetAsync<Sprite>(path).Completed += Card_Completed;
    }
    public void SetProperties(ColorFunc func, CardColor color)
    {
        cardColor = color;
        int spriteIndex = 0;
        switch (color)
        {
            case CardColor.red:
                spriteIndex = 40;
                break;
            case CardColor.yellow:
                spriteIndex = 43;
                break;
            case CardColor.green:
                spriteIndex = 46;
                break;
            case CardColor.blue:
                spriteIndex = 49;
                break;
        }

        switch (func)
        {
            case ColorFunc.Skip:
                cardType = CardType.Skip;
                break;
            case ColorFunc.Reverse:
                cardType = CardType.Reverse;
                spriteIndex += 1;
                break;
            case ColorFunc.PlusTwo:
                cardType = CardType.PlusTwo;
                spriteIndex += 2;
                break;
        }

        sb.Append(unoAllSprite);
        sb.Append("[UNO_ALL_");
        sb.Append(spriteIndex.ToString());
        sb.Append("]");
        string path = sb.ToString();
        Addressables.LoadAssetAsync<Sprite>(path).Completed += Card_Completed;
        inHand = true;
    }
    public void SetProperties(CardColor color, int num)
    {
        cardType = CardType.Number;

        cardColor = color;

        cardNum = num;

        int spriteIndex = 0;

        switch (color)
        {
            case CardColor.red:
                spriteIndex = 0;
                break;
            case CardColor.yellow:
                spriteIndex = 10;
                break;
            case CardColor.green:
                spriteIndex = 20;
                break;
            case CardColor.blue:
                spriteIndex = 30;
                break;
            default:
                break;
        }
        spriteIndex += ((num - 1 + 10) % 10);
        sb.Append(unoAllSprite);
        sb.Append("[UNO_ALL_");
        sb.Append(spriteIndex.ToString());
        sb.Append("]");
        string path = sb.ToString();
        Addressables.LoadAssetAsync<Sprite>(path).Completed += Card_Completed;
        inHand = true;
    }

    // 改变卡牌属性
    public void ChangeProperties(CardInfo info)
    {
        cardInfo = info;

        cardType = info.type;

        cardColor = info.color;

        cardNum = info.number;

        int spriteIndex = 0;

        switch (info.type)

        {
            case CardType.Skip:
                SetProperties(ColorFunc.Skip, cardColor);

                break;

            case CardType.Reverse:
                SetProperties(ColorFunc.Reverse, cardColor);

                break;

            case CardType.PlusTwo:
                SetProperties(ColorFunc.PlusTwo, cardColor);

                break;

            case CardType.WildDrawFour:
                SetProperties(WildFunc.PlusFour);

                break;

            case CardType.Wild:
                SetProperties(WildFunc.Wild);

                break;

            case CardType.Number:
                SetProperties(cardColor, cardNum);

                break;

            default:
                break;
        }
    }

    #endregion


    #region Card Transform presets
    public void SetTransform(Vector3 pos)
    {
        position = pos;
    }

    public void SetRotation(Vector3 rot)
    {
        rotation = rot;
    }

    private void Card_Completed(AsyncOperationHandle<Sprite> obj)
    {
        cardSprite = obj.Result;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = cardSprite;
        }
    }
    
    #endregion
}
