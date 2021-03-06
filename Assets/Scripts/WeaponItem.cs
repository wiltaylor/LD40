﻿using Assets.Scripts;
using UnityEngine;


public enum AttackType
{
    Melee,
    Projectile
}

[CreateAssetMenu(menuName = "ScriptObjects/WeaponItem")]
public class WeaponItem : ScriptableObject
{
    public string Name;
    public Sprite Item;
    public AttackType Type;
    public GameObject WeaponSystem;
    public float Damage;
    public GameObject Projectile;
    public float ProjectileSpeed;
    public GameObject AIGun;
    public AmmoType Ammo;
    public int Priority;
}
