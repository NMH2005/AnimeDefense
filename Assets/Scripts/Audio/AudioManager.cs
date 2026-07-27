using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSrc;
    public static AudioManager Instance;
    private float sfxVolume = 1f;
    private bool sfxMuted = false;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSrc.PlayOneShot(clip, sfxVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void ToggleSFXMute()
    {
        sfxMuted = !sfxMuted;
        sfxSrc.mute = sfxMuted;
    }

    public bool IsSFXMuted() => sfxMuted;
}
