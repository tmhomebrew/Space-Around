using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseItem : MonoBehaviour
{
    public enum ItemType
    {
        ShipPart, TechApp, Weapon, Misc
    }

    #region Fields
    private string bIName;
    private string bIDescription;
    private uint bIInventorySize;
    private ItemType bIItem_type;
    private Sprite bIImage;
    private uint? bIValue; //Can be null
    private bool _hasValue;

    #endregion
    #region Properties
    protected string BIName { get => bIName; set => bIName = value; }
    protected uint? BIValue
    {
        get => bIValue; 
        set
        {
            bIValue = value;
            if (value != null || value >= 0)
            {
                _hasValue = true;
            }
            else
            {
                _hasValue = false;
            }
        }
    }
    protected string BIDescription { get => bIDescription; set => bIDescription = value; }
    protected uint BIInventorySize { get => bIInventorySize; set => bIInventorySize = value; }
    protected ItemType BIItem_type { get => bIItem_type; set => bIItem_type = value; }
    protected Sprite BIImage { get => bIImage; set => bIImage = value; }

    #endregion
    #region Methoeds
    public virtual string GetDescription => BIDescription;
    public virtual string GetName => BIName;
    public virtual uint GetValue
    {
        get
        {
            try
            {
                if (_hasValue)
                {
                    return (uint)BIValue;
                }
                else
                {
                    return 0;
                }
            }
            catch (System.Exception)
            {
                print("Could not return GetValue, cause it is not a value..");
                throw;
            }
        }
    }
    public virtual Sprite GetImage => BIImage;


    public override string ToString()
    {
        return base.ToString() +
            "\nTitle: " + BIName
            + "\n:Description: " + BIDescription;
    }

    #endregion
}

public abstract class Equipment : BaseItem
{
    public enum Rarity
    {
        Common, Uncommon, Rare, Epic, Legendary, Alien, Cosmic
    }

    //public int item_id;
    protected Rarity item_rarity;

    protected Dictionary<string, int> item_stats = new Dictionary<string, int>();

    public Equipment(ItemType typeOfItem, string title, uint value, string description, uint sizeInInventory, 
        Sprite image, Dictionary<string, int> stats)
    {
        BIItem_type = typeOfItem;
        BIName = title;
        BIValue = value;
        BIDescription = description;
        BIInventorySize = sizeInInventory;
        BIImage = image;
        item_stats = stats;
    }
    public Equipment(ItemType typeOfItem, string title, uint value, string description, uint sizeInInventory)
    {
        BIItem_type = typeOfItem;
        BIName = title;
        BIValue = value;
        BIDescription = description;
        BIInventorySize = sizeInInventory;
    }

    public override string ToString()
    {
        return $"This is an {BIItem_type}-Equipment.\n"
            + base.ToString();
    }
}

public abstract class Misc : BaseItem
{
    public Misc(string title, string description, Sprite image, uint? value = null)
    {
        BIItem_type = ItemType.Misc;
        BIInventorySize = 1;
        BIValue = value;

        BIName = title;
        BIDescription = description;
        BIImage = image;
    }

    public override string ToString()
    {
        return $"This is a {BIItem_type}.\n"
            + base.ToString();
    }
}
