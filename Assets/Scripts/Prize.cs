using UnityEngine;
using static PrizeDetectorScript;

[RequireComponent(typeof(AudioSource))]
public class Prize : MonoBehaviour
{
    public PrizeType prizeType;

    private AudioSource audioSource;
    private AudioClip bumpSound;

    public float minVelocityToPlay = 0.1f;
    private Rigidbody2D rb;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        // Load the shared bump sound
        bumpSound = Resources.Load<AudioClip>("Sounds/PlasticBallSFX");

        if (bumpSound == null)
        {
            Debug.LogWarning("PlasticBallSFX not found in Resources/Sounds/");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prize"))
        {
            if (rb.linearVelocity.magnitude > minVelocityToPlay)
            {
                PlayBumpSound();
            }
        }
    }

    void PlayBumpSound()
    {
        if (bumpSound != null)
        {
            audioSource.PlayOneShot(bumpSound);
            audioSource.volume = 0.5f;
        }
    }
}