using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class IBaseItem
{
    public enum Rarity
    {
        Common, UnCommon, Rare, Epic, Legendary, Alien, Cosmic
    }
    public enum ItemType
    {
        Money, ShipPart, TechApp,
    }

    private string _BIName;
    protected int _BIValue;
    protected int _BIValueVendorCost;

    protected Rarity _BIRarity;
    protected ItemType _BITypeOfItem;


}
