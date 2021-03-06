﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<QuestItem>  QuestItems = new List<QuestItem>();
    public Camera MainCamera;
    public float UseDistance = 10f;
    public int Gold;
    public PlayerStats Stats;

    public Dictionary<AmmoType, int> Ammo = new Dictionary<AmmoType, int>();
    public List<WeaponItem> Weapons = new List<WeaponItem>();

    private bool _updatePlayerFromGameManager;

    void Start()
    {

        _updatePlayerFromGameManager = true;

        Stats.QuestItems = QuestItems;
        Stats.Weapons = Weapons;
    }

    public void PickupQuestItem(QuestItem item)
    {
        if(!QuestItems.Contains(item))
            QuestItems.Add(item);
    }

    public void PickUpAmmo(AmmoPickUp pickup)
    {
        if (Ammo.ContainsKey(pickup.Type))
        {
            Ammo[pickup.Type] += pickup.Ammount;

            if (Ammo[pickup.Type] > pickup.Type.MaxCarry)
                Ammo[pickup.Type] = pickup.Type.MaxCarry;
        }
        else
        {
            Ammo.Add(pickup.Type, pickup.Ammount);
        }
    }

    public void PickUpGold(int ammount)
    {
        Gold += ammount;
        Stats.Score = Gold;
    }

    public void PickUpWeapon(WeaponItem weapon)
    {
        if(!Weapons.Contains(weapon))
            Weapons.Add(weapon);
    }

    void Update()
    {

        if (GameManger.Instance != null && _updatePlayerFromGameManager)
        {
            _updatePlayerFromGameManager = false;
            GameManger.Instance.RestorePlayer(this);
        }

        if (Input.GetButton("Use"))
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, MainCamera.transform.forward, Color.green, 10f, true);

            if (Physics.Raycast(transform.position, MainCamera.transform.forward, out hit, UseDistance))
            {
                if (hit.transform.CompareTag("Usable"))
                {
                    hit.transform.gameObject.SendMessage("Use", QuestItems);
                }
                else if (hit.transform.CompareTag("ParentUsable"))
                {
                    hit.transform.parent.gameObject.SendMessage("Use", QuestItems);
                }
                else
                {
                    Debug.Log("Not a valid hit target!");
                }

            }

            else
            {
                //can't use so grunt or something.
                Debug.Log("No hit");
            }
        }
    }

}
