﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : BaseItem, IWeapon
{
    [SerializeField]
    string _laserName;
    [SerializeField]
    Rarity _laserRarity;
    [SerializeField]
    int _laserDamage;

    [SerializeField]
    public Sprite[] laserBeamSprite = new Sprite[5];
    Sprite mySprite;


    //LaserShot - Not Implemented
    public GameObject laserShot;
    GameObject laserShotOwner;
    public GameObject spawnPoint;
    public Transform laserHolder;
    public GameObject LaserShotOwner { get => laserShotOwner; set => laserShotOwner = value; }

    public WeaponLaser(string name, Rarity quality, ItemType weapon = ItemType.Weapon)
    {
        _BIName = name;
        _BIRarity = quality;
        _BIValue = ValueOfLaser(_BIRarity);
        _BITypeOfItem = weapon;
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
                value *= 10;
                break;
            case Rarity.UnCommon:
                value *= 20;
                break;
            case Rarity.Rare:
                value *= 50;
                break;
            case Rarity.Epic:
                value *= 70;
                break;
            case Rarity.Legendary:
                value *= 100;
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

    private void LaserShotType(Rarity laserWeaponQuality)
    {

    }
}