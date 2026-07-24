using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject btnPanel;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private TextMeshProUGUI btnText;
    private TowerBaseSlot currentSlot;
    private void Awake()
    {
        btnPanel.SetActive(false);
        upgradeBtn.onClick.AddListener(OnUpgradeClicked);
    }

    private void OnUpgradeClicked()
    {
        currentSlot.TryUpgrade();
        btnPanel.SetActive(false);
    }

    public void Show(TowerBaseSlot slot)
    {
        currentSlot = slot;
        btnPanel.SetActive(true);
        WeaponData data = slot.GetWeaponData();
        int cost = data.GetUpgradeCost(slot.CurrentLvl);
        btnText.text = $"UPGRADE\n{cost}";
    }

}
