using UnityEngine;
using UnityEngine.EventSystems; // Required for event interfaces

public class RightButtonHoldDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed = false;
    Rigidbody2D clawRigidbody;
    Rigidbody2D clawConnector;
    public AudioSource clawmovingSFX;
    public AudioSource buttonSFX;
    [SerializeField] private float maxLeanAngle = 15f;
    [SerializeField] private float leanSpeed = 4f;
    void Start()
    {
        GameObject clawBody = GameObject.Find("ClawBody");
        clawRigidbody = clawBody.GetComponent<Rigidbody2D>();
        GameObject clawBodyC = GameObject.Find("ConnectorBar");
        clawConnector = clawBodyC.GetComponent<Rigidbody2D>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        clawmovingSFX.Play();
        buttonSFX.Play();
        Debug.Log("Button pressed down (or tapped down).");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        clawmovingSFX.Stop();
        buttonSFX.Stop();
        Debug.Log("Button released.");
    }

    void FixedUpdate()
    {
        if (isButtonPressed)
        {
            clawRigidbody.AddForce(Vector3.right * 5f, ForceMode2D.Force);
            clawConnector.AddForce(Vector3.right * 5f, ForceMode2D.Force);
            // Code here runs every frame the button is held
            Debug.Log("Button is being held...");
        }
        else
        {

            clawRigidbody.linearVelocity *= new Vector2(0.92f, 1f);//trying to lessen the speed when not holding the button
            clawConnector.linearVelocity *= new Vector2(0.92f, 1f);//trying to lessen the speed when not holding the button


        }
        ClawBodySimulatePhycis(clawRigidbody);
        ClawBodySimulatePhycis(clawConnector);
    }

    void ClawBodySimulatePhycis(Rigidbody2D rigid)
    {
        // ── Sway / lean effect ──────────────────────────────────
        float horizontalSpeed = rigid.linearVelocity.x;

        // When going left (negative x), we want positive rotation (lean right in most 2D setups)
        // Adjust multiplier sign if your sprite leans the wrong way
        float targetAngle = horizontalSpeed * 3f;               // tune 3f → more/less sensitive
        targetAngle = Mathf.Clamp(targetAngle, -maxLeanAngle, maxLeanAngle);

        // Smoothly rotate toward target
        float currentAngle = rigid.rotation;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, leanSpeed * Time.fixedDeltaTime * 60f);

        rigid.MoveRotation(newAngle);
    }
}
