using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayGameMusic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private AudioSource musicSource;

    public bool playmusic = true;

    public Sprite musicUIOn;
    public Sprite musicUIOff;
    public Image musicImage;

    public AudioSource musicbuttonSFX;

    public void OnPointerDown(PointerEventData eventData)
    {
        playmusic = !playmusic; // toggle
        musicbuttonSFX.Play();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        musicbuttonSFX.Stop();
    }
        private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayM();
    }

    void PlayM()
    {
        if (playmusic)
        {
            if (!musicSource.isPlaying)
                musicSource.UnPause();

            musicImage.sprite = musicUIOn;
        }
        else
        {
            if (musicSource.isPlaying)
                musicSource.Pause();

            musicImage.sprite = musicUIOff;
        }
    }
}