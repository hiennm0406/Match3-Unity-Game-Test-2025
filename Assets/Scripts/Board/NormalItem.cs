using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{
    public enum eNormalType
    {
        TYPE_ONE,
        TYPE_TWO,
        TYPE_THREE,
        TYPE_FOUR,
        TYPE_FIVE,
        TYPE_SIX,
        TYPE_SEVEN
    }

    public eNormalType ItemType;

    public override void SetView()
    {
        GameManager m_gmr = GameManager.Instance;
        ViewSr = Pooling.Instance.PullItem();
        ViewSr.sprite = m_gmr.DataSetting.lstSkin[m_gmr.GameSetting.ItemSkin].NormalTypes[(int)ItemType];
        base.SetView();
    }

    public void SetType(eNormalType type)
    {
        ItemType = type;
    }

    internal override bool IsSameType(Item other)
    {
        NormalItem it = other as NormalItem;

        return it != null && it.ItemType == this.ItemType;
    }
}
