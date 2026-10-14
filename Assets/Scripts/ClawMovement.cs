using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClawMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PrizeDetectorScript prizedescription;

    public Rigidbody2D clawTransL;
    public Rigidbody2D clawTransR;
    public Rigidbody2D clawTransB;
    public Rigidbody2D clawConnector;
    public AudioSource clawOpenSFX;
    public AudioSource clawCloseSFX;
    public AudioSource clawMoveSFX;
    public AudioSource buttonSFX;
    public float moveSpeed = 0.01f;
    public float rotationSpeed = 0.1f;
    public float NegativerotationSpeed = -0.1f;

    public bool clawsClosed = false;
    public bool clawReturning = false;
    public bool isButtonPressed = false;
    public bool clawAtTop;
    //Temporary Make another script to dettect what prizes got pick up
    public bool playPrizeAnimation;
    public GameObject PrizeGameObj;

    public Button leftButtn;
    public Button rightButtn;
    void Start()
    {
        clawTransL = GameObject.Find("ClawLeft").GetComponent<Rigidbody2D>();
        clawTransR = GameObject.Find("ClawRight").GetComponent<Rigidbody2D>();
        clawTransB = GameObject.Find("ClawBody").GetComponent<Rigidbody2D>();

        GameObject clawBodyC = GameObject.Find("ConnectorBar");
        clawConnector = clawBodyC.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isButtonPressed)
        {
            MoveClawDown();
        }
        else if (clawReturning)
        {
            MoveClawUp();
        }

        if(isButtonPressed)
        {
            leftButtn.interactable = false;
            rightButtn.interactable = false;
        }
        else
        {
            if(clawAtTop)
            {
                leftButtn.interactable = true;
                rightButtn.interactable = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isButtonPressed && !clawReturning)
        {
            //Temporary Make another script to dettect what prizes got pick up
            playPrizeAnimation = false;
            clawAtTop = false;
            isButtonPressed = true;
            PrizeGameObj.SetActive(false);
            clawMoveSFX.Play();
            buttonSFX.Play();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonSFX.Stop();
    }
    

    void MoveClawDown()
    {
        if (clawTransB.transform.position.y > 2f)
        {
            clawConnector.constraints = RigidbodyConstraints2D.FreezeAll;
            clawTransB.transform.Translate(0, -moveSpeed, 0);
        }
        else
        {
            clawMoveSFX.Stop();
            clawAtTop = false;
            clawTransB.transform.position = new Vector2(clawTransB.transform.position.x, 2f);
            clawTransB.linearVelocity = Vector2.zero;
            RotateClaws();
        }
    }

    void MoveClawUp()
    {
        if (!clawMoveSFX.isPlaying)
            clawMoveSFX.Play();

        if (clawTransB.transform.position.y < 5.9f)
        {
            clawTransB.transform.Translate(0, moveSpeed, 0);
        }
        else
        {
            clawMoveSFX.Stop();
            clawAtTop = true;

            clawConnector.constraints = RigidbodyConstraints2D.None;
            clawTransB.transform.position = new Vector2(clawTransB.transform.position.x, 5.9f);
            clawTransB.linearVelocity = Vector2.zero;

            OpenClaws();

            if (!playPrizeAnimation && prizedescription != null && prizedescription.hasGottenPrize)
            {
                PrizeGameObj.SetActive(true); playPrizeAnimation = true; // 🔥 THIS is the key fix
                prizedescription.ClaimPrize();
                if (!clawOpenSFX.isPlaying)
                    clawOpenSFX.Play();
            }
        }
    }

    void RotateClaws()
    {
        Debug.Log("clsing claws");
        if (!clawCloseSFX.isPlaying)
            clawCloseSFX.Play();
        // LEFT CLAW
        float leftZ = clawTransL.transform.localEulerAngles.z;
        if (leftZ > 180) leftZ -= 360;

        if (leftZ < 20)
        {
            clawTransL.transform.Rotate(0, 0, rotationSpeed);
        }
        else
        {
            clawTransL.freezeRotation = true;
        }

        // RIGHT CLAW
        float rightZ = clawTransR.transform.localEulerAngles.z;
        if (rightZ > 180) rightZ -= 360;

        if (rightZ > -20)
        {
            clawTransR.transform.Rotate(0, 0, -rotationSpeed);
        }
        else
        {
            clawCloseSFX.Stop();
            clawTransR.freezeRotation = true;
        }

        // Once claws reach target rotation, start returning
        if (leftZ >= 20 && rightZ <= -20)
        {
            clawsClosed = true;
            isButtonPressed = false;
            clawReturning = true;
        }
    }
    void OpenClaws()
    {
        
        float leftZ = clawTransL.transform.localEulerAngles.z;
        if (leftZ > 180) leftZ -= 360;

        float rightZ = clawTransR.transform.localEulerAngles.z;
        if (rightZ > 180) rightZ -= 360;

        bool leftDone = false;
        bool rightDone = false;

        // LEFT CLAW (open back)
        if (leftZ > -20)
        {
            clawTransL.transform.Rotate(0, 0, NegativerotationSpeed);
        }
        else
        {
            leftDone = true;
        }

        // RIGHT CLAW (open back)
        if (rightZ < 20)
        {
            clawTransR.transform.Rotate(0, 0, -NegativerotationSpeed);
        }
        else
        {
            rightDone = true;
        }

        // DONE OPENING
        if (leftDone && rightDone)
        {
             
            clawsClosed = false; // ✅ FIXED
            clawReturning = false;
        }
    }
}