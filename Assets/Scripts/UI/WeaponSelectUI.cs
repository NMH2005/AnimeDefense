using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour {
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] weaponBtns;
    [SerializeField] private WeaponData[] weapons;

    private TowerBaseSlot targetSlot;

    private void Awake()
    {
        panel.SetActive(false);
        for (int i = 0; i < weaponBtns.Length; i++)
        {
            WeaponData weapon = weapons[i];
            weaponBtns[i].GetComponentInChildren<Text>().text = $"{weapon.name}\n{weapon.baseCost}";
            weaponBtns[i].onClick.AddListener(() => OnWeaponChosen(weapon));
        }
    }

    private void OnWeaponChosen(WeaponData data)
    {
        targetSlot.PlaceWeapon(data);
        panel.SetActive(false);
    }
}
