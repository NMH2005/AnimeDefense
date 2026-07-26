using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI goldText;

   private void OnEnable()
    {
        EventManager.OnGoldChanged += HandleGoldChanged;
        if (GoldManager.Instance != null)
            HandleGoldChanged(GoldManager.Instance.Gold);
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
