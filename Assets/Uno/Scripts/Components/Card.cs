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
    public CardVisual cardVisual;
    public CardType cardType;
    public CardColor cardColor;
    public int cardNum = -1;
    public CardInfo cardInfo;
    bool inHand = false;
    bool onDrag = false;
    SpriteRenderer spriteRenderer;
    Color hovered = new Color(0.75f, 0.75f, 0.75f);
    string unoAllSprite = "UNO_ALL.png";
    StringBuilder sb = new StringBuilder();
    Sprite cardSprite = null;
    public GameObject cardVisualPrefab;
    Vector3 position = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    private void Awake()
    {
        cardVisual = Instantiate(cardVisualPrefab).GetComponent<CardVisual>();
        spriteRenderer = cardVisual.spriteRenderer;
        

        if (cardSprite != null)
        {
            spriteRenderer.sprite = cardSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void ColorReset()
    {
        spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// 设置卡牌的点击回调函数
    /// </summary>

    /// <param name="func"></param>
    public void SetProperties(WildFunc func)
    {
        string temp = null;

        sb.Append(unoAllSprite);

        switch (func)
        {
            case WildFunc.PlusFour:
                cardType = CardType.PlusFour;

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

    /// <summary>
    /// 异步加载卡牌精灵资源
    /// </summary>

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
    }

    /// <summary>
    /// 设置卡牌的颜色效果
    /// </summary>

    /// <param name="color"></param>
    /// <param name="num"></param>
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

            case CardType.PlusFour:
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

    public void SetTransform(Vector3 pos)
    {
        position = pos;

        transform.DOMove(position, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void SetRotation(Vector3 rot)
    {
        rotation = rot;

        transform.DORotate(rotation, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void BackToPosition()
    {
        transform.DOMove(position, 0.2f).SetEase(Ease.InOutQuad);
    }

    private void Card_Completed(AsyncOperationHandle<Sprite> obj)
    {
        cardSprite = obj.Result;

        if (spriteRenderer != null)

        {
            spriteRenderer.sprite = cardSprite;
        }
    }
}
