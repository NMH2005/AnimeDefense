using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerPlace : MonoBehaviour {
    [SerializeField] private LayerMask baseLayer;
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponSelectUI weaponSelectUI;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }
    public void OnClickGround(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;

        TryPlaceTower();
    }

    private void TryPlaceTower()
    {

        Vector2 screenPos = Pointer.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, baseLayer))
        {
            TowerBaseSlot slot = hit.collider.GetComponent<TowerBaseSlot>();
            if (slot == null) return;
            if (slot.isOccupied) return;

            weaponSelectUI.Show(slot);
        }


    }
}
