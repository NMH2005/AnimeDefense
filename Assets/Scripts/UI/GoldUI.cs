using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI goldText;

    private void Start()
    {
        goldText.text = GoldManager.Instance.Gold.ToString();
    }
    private void OnEnable()
    {
        EventManager.OnGoldChanged += HandleGoldChanged;
    }

    private void OnDisable()
    {
        EventManager.OnGoldChanged -= HandleGoldChanged;
    }

    private void HandleGoldChanged(int newGold)
    {
        goldText.text = newGold.ToString();
    }
}
