using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Equipment
{
    //Fields
    [SerializeField]
    public Sprite[] laserBeamSprites = new Sprite[5];

    public enum LaserType
    {
        Green,
        LightBlue,
        Blue,
        Yellow,
        Red,
        Purple
    }
    //Properties

    /// <summary>
    /// Laser Weapon
    /// </summary>
    /// <param name="laserType">What type of laser it is, on a range from 'Green' to 'Purple'.</param>
    public ItemWeapon(LaserType laserType) 
        : base(ItemType.Weapon,
            "LaserWeapon",
            1, 
            "This is a weapon that shoots lasers. Damage is depended on rarity, and can be seen by the color.",
            1)
    {
        BIItem_type = ItemType.Weapon;
        BIImage = laserBeamSprites[(int)laserType];
        //Stats for LaserShot
        switch (laserType)
        {
            case LaserType.Green:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 1 },
                    {"Speed",  20 }
                };
                BIValue = 10;
                BIDescription =
                    "Most common laser, Green.";
                break;
            case LaserType.LightBlue:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 2 },
                    {"Speed",  20 }
                };
                BIValue = 50;
                BIDescription =
                    "Uncommen laser, Lightblue.";
                break;
            case LaserType.Blue:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 4 },
                    {"Speed",  25 }
                };
                BIValue = 400;
                BIDescription =
                    "Rare laser, Dark Blue.";
                break;
            case LaserType.Yellow:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 6 },
                    {"Speed",  25 }
                };
                BIValue = 2500;
                BIDescription =
                    "Epic laser, Yellow.";
                break;
            case LaserType.Red:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 12 },
                    {"Speed",  40 }
                };
                BIValue = 30000;
                BIDescription =
                    "Legendary stored energy, Red.";
                break;
            case LaserType.Purple:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 20 },
                    {"Speed",  50 }
                };
                BIValue = 100000;
                BIDescription =
                    "An Alien laser, Purple.";
                break;
            default:
                item_stats = new Dictionary<string, int>
                {
                    {"Damage", 1 },
                    {"Speed",  1 }
                };
                BIValue = 1;
                BIDescription =
                    "Most common laser, Green.";

                break;
        }

        GameObject laserShot = new GameObject();
        laserShot.AddComponent<LaserShot>();
        laserShot.GetComponent<LaserShot>().Damage = item_stats["Damage"];
        laserShot.GetComponent<LaserShot>().Speed = item_stats["Speed"];
    }
}
