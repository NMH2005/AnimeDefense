using UnityEngine;

public class TowerBaseSlot : MonoBehaviour {
    public bool isOccupied = false;
    [SerializeField] private Transform mountPoint;
    private GameObject currentTower;

    public Vector3 GetMountPosition()
    {
        return mountPoint != null ? mountPoint.position : transform.position;
    }

    public void PlaceWeapon(WeaponData data)
    {
        if (isOccupied) return;
        currentTower = Instantiate(data.levelPrefabs[0], GetMountPosition(), Quaternion.Euler(0,270,0));
        isOccupied = true;
    }
}