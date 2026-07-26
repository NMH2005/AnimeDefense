using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject btnPanel;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private TextMeshProUGUI upgradeBtnText;
    [SerializeField] private TextMeshProUGUI sellBtnText;
    private TowerBaseSlot currentSlot;
    private void Awake()
    {
        btnPanel.SetActive(false);
        upgradeBtn.onClick.AddListener(OnUpgradeClicked);
        sellBtn.onClick.AddListener(OnSellClicked);
    }

    private void OnUpgradeClicked()
    {
        currentSlot.TryUpgrade();
        btnPanel.SetActive(false);
    }

    private void OnSellClicked()
    {
        currentSlot.RemoveWeapon();
        btnPanel.SetActive(false);
    }

    public void Show(TowerBaseSlot slot)
    {
        currentSlot = slot;
        btnPanel.SetActive(true);
        WeaponData data = slot.GetWeaponData();
        int cost = data.GetUpgradeCost(slot.CurrentLvl);
        int val = data.GetSellValue(slot.CurrentLvl);
        upgradeBtnText.text = $"UPGRADE\n{cost}";
        sellBtnText.text = $"SELL\n{val}";

    }

    public void Hide()
    {
        if (btnPanel.activeInHierarchy) 
        {
            btnPanel.SetActive(false);
            currentSlot = null;
        }
    }


}
