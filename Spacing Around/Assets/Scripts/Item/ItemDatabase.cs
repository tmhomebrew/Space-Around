using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<BaseItem> dbItems = new List<BaseItem>();

    private void Awake()
    {
        BuildDatabase();
    }

    //public Equipment GetItem(int id)
    //{
    //    return dbItems.Find(item => item.item_id == id);
    //}
    //public Equipment GetItem(string itemName)
    //{
    //    return dbItems.Find(item => item.item_title == itemName);
    //}

    void BuildDatabase()
    {
        //dbItems = new List<Equipment> {
        //    new ItemWeapon(0, ItemWeapon.LaserType2.Green, "Starter Laser",
        //    new Dictionary<string, int>
        //    {
        //        {"ItemValue", 1 },
        //        {"Power", 1 }
        //    }, "Green Laser"),
        //    new ItemWeapon(1, ItemWeapon.LaserType2.LightBlue, "Blueish Laser",
        //    new Dictionary<string, int>
        //    {
        //        {"ItemValue", 50 },
        //        {"Power", 2 }
        //    }, "Blue Laser"),
        //    new ItemWeapon(2, ItemWeapon.LaserType2.Blue, "Dark Blue Laser shot",
        //    new Dictionary<string, int>
        //    {
        //        {"ItemValue", 300 },
        //        {"Power", 4 }
        //    }, "DarkBlue Laser"),
        //    new ItemWeapon(3, ItemWeapon.LaserType2.Yellow, "Yellow lightning Laser",
        //    new Dictionary<string, int>
        //    {
        //        {"ItemValue", 2000 },
        //        {"Power", 6 }
        //    }, "Yellow Laser"),
        //    new ItemWeapon(4, ItemWeapon.LaserType2.Red, "Alien Laser",
        //    new Dictionary<string, int>
        //    {
        //        {"ItemValue", 10000 },
        //        {"Power", 12 }
        //    }, "Red Laser"),
        //    new ItemWeapon(5, ItemWeapon.LaserType2.Purple, "Cosmic Laser",
        //    new Dictionary<string, int>
        //    {
        //        {"ItemValue", 100000 },
        //        {"Power", 20 }
        //    }, "Purple Laser"),
        //};
    }
}