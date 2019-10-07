using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public enum Rarity
    {
        Common, UnCommon, Rare, Epic, Legendary, Alien, Cosmic
    }
    public enum ItemType
    {
        Money, ShipPart, TechApp, Weapon
    }

    protected string _BIName;
    protected uint _BIValue;
    protected uint _BIPriceEdit;
    protected uint _BIValueVendorCost;

    protected Rarity _BIRarity;
    protected ItemType _BITypeOfItem;


}
