using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField] private int gold = 200;
    public int Gold => gold;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool CanAfford(int amount)
    {
        return gold >= amount;
    }

    public void Spend(int amount)
    {
        gold -= amount;
        EventManager.RaiseGoldChanged(gold);
    }

    public void Add(int amount)
    {
        gold += amount;
        EventManager.RaiseGoldChanged(gold);
    }



}
