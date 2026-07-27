using UnityEngine;
using UnityEngine.UI;

public class SFXToggleButton : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        UpdateIcon();
    }

    private void OnClick()
    {
        AudioManager.Instance.ToggleSFXMute();
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        bool muted = AudioManager.Instance.IsSFXMuted();
        icon.sprite = muted ? offSprite : onSprite;
    }
}