using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject healthBarRoot;
    public void SetHealth(float current, float max)
    {
        if (fillImage == null) return;
        fillImage.fillAmount = Mathf.Clamp01(current / max);
    }

    public void Hide()
    {
        if (healthBarRoot != null)
            healthBarRoot.SetActive(false);
    }
}
