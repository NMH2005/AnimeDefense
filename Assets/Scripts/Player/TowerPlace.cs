using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerPlace : MonoBehaviour {
    [SerializeField] private LayerMask baseLayer;
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponSelectUI weaponSelectUI;
    [SerializeField] private UpgradeUI upgradeUI;


    private bool isPointerOverUI;
    private bool clickRequested;
    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        isPointerOverUI = EventSystem.current.IsPointerOverGameObject();

        if (clickRequested)
        {
            clickRequested = false;
            TryPlaceTower();
        }
    }

    public void OnClickGround(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        clickRequested = true;
    }

    private void TryPlaceTower()
    {
        if (isPointerOverUI) return;
        Vector2 screenPos = Pointer.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, baseLayer))
        {
            TowerBaseSlot slot = hit.collider.GetComponent<TowerBaseSlot>();
            if (slot == null) return;
            if (slot.isOccupied)
            {
                weaponSelectUI.Hide();
                upgradeUI.Show(slot);
            }
            else
            {
                upgradeUI.Hide();
                weaponSelectUI.Show(slot);
            }

        }


    }
}
