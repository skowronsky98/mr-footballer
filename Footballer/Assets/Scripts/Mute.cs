using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{

    [SerializeField]
    public Sprite muteIcon, unMuteIcon;
    [SerializeField]
    public Image muteImage, musicImage;
    [SerializeField]
    public Color unMuteColor, muteColor;

    public static bool muteSounds;
    public static bool muteMusic;

      public static Mute Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        muteSounds = SecurePlayerPrefs.GetBool("MuteSound", muteSounds);
        muteMusic = SecurePlayerPrefs.GetBool("MuteMusic", muteMusic);
               
        muteImage.sprite = muteIcon;

        if (muteSounds)
        {
            muteImage.sprite = unMuteIcon;
            muteImage.color = unMuteColor;
        }
        else
        {
            musicImage.color = muteColor;
            muteImage.color = muteColor;
        }

        if (muteMusic)
        {
            musicImage.color = unMuteColor;
        }
        else
        {
            musicImage.color = muteColor;
        }
    }

        public void MuteSounds()
    {
        if (muteImage.sprite == muteIcon)
        {
            muteImage.sprite = unMuteIcon;
            muteImage.color = unMuteColor;

            muteSounds = true;
            SecurePlayerPrefs.SetBool("MuteSound", true);


        }
        else if (muteImage.sprite == unMuteIcon)
        {
            muteImage.sprite = muteIcon;
            muteImage.color = muteColor;

            muteSounds = false;
            SecurePlayerPrefs.SetBool("MuteSound", false);

        }

    }

    public void MuteMusic()
    {
        if (musicImage.color == muteColor)
        {
            musicImage.color = unMuteColor;

            muteMusic = true;
                        
            SecurePlayerPrefs.SetBool("MuteMusic", true);
        }
        else if (musicImage.color == unMuteColor)
        {
            musicImage.color = muteColor;

            muteMusic = false;

            SecurePlayerPrefs.SetBool("MuteMusic", false);

        }
    }

    public void MuteMusicInGame()
    {
        if (musicImage.color == muteColor)
        {
            musicImage.color = unMuteColor;

            muteMusic = true;
            SecurePlayerPrefs.SetBool("MuteMusic", true);

            UIManager.uiManagerInstance.musicAudio.enabled = false;
        }
        else if (musicImage.color == unMuteColor)
        {
            musicImage.color = muteColor;

            muteMusic = false;
            SecurePlayerPrefs.SetBool("MuteMusic", false);

            UIManager.uiManagerInstance.musicAudio.enabled = true;

        }

    }

}
