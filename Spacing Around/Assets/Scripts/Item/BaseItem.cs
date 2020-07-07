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
        ShipPart, TechApp, Weapon
    }

    protected string _BIName;
    protected uint _BIValue;
    protected uint _BIPriceEdit;
    private uint bIValueVendorCost;
    protected uint _BIWeight;

    protected object _BIImage;

    protected Rarity _BIRarity;
    protected ItemType _BITypeOfItem;

    protected uint BIValueVendorCost
    {
        get => bIValueVendorCost; 
        private set
        {
            //bIValueVendorCost = value;
            bIValueVendorCost = _BIValue / 10;
        }
    }
}

public class ItemWeapon : BaseItem
{
    //Fields

    //Properties

    public ItemWeapon(string name = "NoName")
    {
        _BITypeOfItem = ItemType.Weapon;

        _BIName = name;

    }
}