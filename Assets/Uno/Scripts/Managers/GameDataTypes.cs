using System.Collections;

using System.Collections.Generic;

using System.Diagnostics;

using UnityEngine;

public enum ColorFunc

{
    None=0,
    Skip=1,
    Reverse=2,
    PlusTwo=3,
}

public enum CardColor

{
    None = 0,
    green=1,
    blue=2,
    yellow=3,
    red=4,
    wild=5,
}

public enum WildFunc

{
    None=0,
    Wild=1,
    PlusFour=2,
}

public enum CardType

{
    None=0,
    Skip = 1,
    Reverse = 2,
    PlusTwo = 3,
    PlusFour=4,
    Wild=5,
    Number=6,
}

//public enum Color { Red, Yellow, Blue, Green, Wild }

//public enum Type { Number, Skip, Reverse, PlusTwo, Wild, PlusFour }

public enum WrapperType

{
    Heartbeat = 0,
    ConnectReq = 1,
    DisconnectReq = 2,
    ChangeNickname_IconReq = 3,
    PlayCardReq = 4,
    PlusCardReq = 5,
    GuessCardReq = 6,
    SyncReq = 7,
    EmojiReq = 8,
    TransferHostReq = 9,
    
}

[System.Serializable]
public class CardInfo

{
    public CardColor color;

    public CardType type;

    public int number;

    public CardInfo(CardColor color = CardColor.None, CardType type = CardType.None, int number = -1)
    {
        this.color = color;

        this.type = type;

        this.number = number;
    }

    public CardInfo(CardColor color = CardColor.None, ColorFunc type = ColorFunc.None, int number = -1)
    {
        this.color = color;

        switch (type)

        {
            case ColorFunc.Skip:
                this.type = CardType.Skip;

                break;

            case ColorFunc.Reverse:
                this.type = CardType.Reverse;

                break;

            case ColorFunc.PlusTwo:
                this.type = CardType.PlusTwo;

                break;

            default:
                break;
        }

        this.number = number;
    }

    public CardInfo(WildFunc type = WildFunc.None)
    {
        this.color = CardColor.None;

        switch (type)

        {
            case WildFunc.PlusFour:
                this.type = CardType.PlusFour;

                break;

            case WildFunc.Wild:
                this.type = CardType.Wild;

                break;

            default:
                break;
        }

        this.number = -1;
    }

    public CardInfo()
    {
        color = CardColor.None;
        type = CardType.None;
        number = -1;
    }

    public bool Campare(CardInfo info)
    {
        if (info.color == this.color && info.number == this.number && info.type == this.type)
            return true;
        return false;
    }
}

