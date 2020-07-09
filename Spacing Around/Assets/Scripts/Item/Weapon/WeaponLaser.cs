using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class WeaponLaser : Equipment, IWeaponShoot
//{
//    [SerializeField]
//    string _laserName;
//    [SerializeField]
//    int _laserDamage;

//    //LaserShot - Not Implemented
//    public GameObject laserShot;
//    GameObject laserShotOwner;
//    public GameObject spawnPoint;
//    public Transform laserHolder;

//    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }
//    public int LaserDamage { get => _laserDamage; set => _laserDamage = value; }

//    //public WeaponLaser(string name, Rarity quality, ItemType weapon = ItemType.Weapon)
//    //{
//    //    BIName = name;
//    //    BIRarity = quality;
//    //    BIValue = ValueOfLaser(_BIRarity);
//    //    _BITypeOfItem = weapon;
        
//    //    _BIWeight = 0; //Not set

//    //    //laserShot = 
//    //}

//    public void Shoot()
//    {
//        Instantiate(laserShot, spawnPoint.transform.position, transform.rotation, laserHolder);
//    }

//    uint value;
//    private uint ValueOfLaser(Equipment.Rarity quality)
//    {
//        value = 1;
//        switch (quality)
//        {
//            case Equipment.Rarity.Common:
//                value *= 2;
//                break;
//            case Equipment.Rarity.Uncommon:
//                value *= 10;
//                break;
//            case Equipment.Rarity.Rare:
//                value *= 50;
//                break;
//            case Equipment.Rarity.Epic:
//                value *= 200;
//                break;
//            case Equipment.Rarity.Legendary:
//                value *= 2500;
//                break;
//            case Equipment.Rarity.Alien:
//                value *= 5000;
//                break;
//            case Equipment.Rarity.Cosmic:
//                value *= 100000;
//                break;
//            default:
//                break;
//        }
//        return value;
//    }
//}
