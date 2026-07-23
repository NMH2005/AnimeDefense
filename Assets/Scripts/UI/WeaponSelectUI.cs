using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour {
    [SerializeField] private RectTransform panelRoot;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] weaponBtns;
    [SerializeField] private WeaponData[] weapons;
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvas;
    private TowerBaseSlot targetSlot;
    private Animator animator;

    private void Awake()
    {
        animator = panel.GetComponent<Animator>();
        if (cam == null) cam = Camera.main;
        panel.SetActive(false);
        for (int i = 0; i < weaponBtns.Length; i++)
        {
            WeaponData weapon = weapons[i];
            weaponBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{weapon.weaponName}\n{weapon.baseCost}";
            weaponBtns[i].onClick.AddListener(() => OnWeaponChosen(weapon));
        }
    }

    public void Show(TowerBaseSlot slot)
    {
        targetSlot = slot;
        Vector3 worldPos = slot.transform.position + Vector3.up * 2f;
        Vector2 screenPos = cam.WorldToScreenPoint(worldPos);
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        panelRoot.position = screenPos;

        panel.SetActive(true);

        animator.Rebind();
        animator.Update(0);


        animator.Play("Show", 0, 0f);

    }

    private void OnWeaponChosen(WeaponData data)
    {
        targetSlot.PlaceWeapon(data);

        StartCoroutine(HidePanel());
    }

    private IEnumerator HidePanel()
    {
        animator.Play("Hide", 0, 0f);

        yield return new WaitForSeconds(0.15f);

        panel.SetActive(false);
    }
}
