using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : BaseItem, IWeaponShoot
{
    [SerializeField]
    string _laserName;
    [SerializeField]
    Rarity _laserRarity;
    [SerializeField]
    int _laserDamage;

    //LaserShot - Not Implemented
    public GameObject laserShot;
    GameObject laserShotOwner;
    public GameObject spawnPoint;
    public Transform laserHolder;

    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }
    public Rarity LaserRarity { get => _laserRarity; set => _laserRarity = value; }
    public int LaserDamage { get => _laserDamage; set => _laserDamage = value; }

    public WeaponLaser(string name, Rarity quality, ItemType weapon = ItemType.Weapon)
    {
        _BIName = name;
        _BIRarity = quality;
        _BIValue = ValueOfLaser(_BIRarity);
        _BITypeOfItem = weapon;
        
        _BIWeight = 0; //Not set

        //laserShot = 
    }

    public void Shoot()
    {
        Instantiate(laserShot, spawnPoint.transform.position, transform.rotation, laserHolder);
    }

    uint value;
    private uint ValueOfLaser(Rarity quality)
    {
        value = 1;
        switch (quality)
        {
            case Rarity.Common:
                value *= 2;
                break;
            case Rarity.UnCommon:
                value *= 10;
                break;
            case Rarity.Rare:
                value *= 50;
                break;
            case Rarity.Epic:
                value *= 200;
                break;
            case Rarity.Legendary:
                value *= 2500;
                break;
            case Rarity.Alien:
                value *= 5000;
                break;
            case Rarity.Cosmic:
                value *= 100000;
                break;
            default:
                break;
        }
        return value;
    }
}
