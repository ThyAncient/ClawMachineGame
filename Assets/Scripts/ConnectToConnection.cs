using UnityEngine;

public class ConnectToConnection : MonoBehaviour
{
    [SerializeField] private Transform target;           // drag "ConnectionPoint" here in Inspector
    [SerializeField] private bool useLateUpdate = true;  // LateUpdate usually smoother for visual follow

    private Rigidbody2D myRb;                            // optional - if this object has physics

    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            GameObject conn = GameObject.Find("ConnectionPoint");
            if (conn != null)
            {
                target = conn.transform;
            }
            else
            {
                Debug.LogError("ConnectionPoint not found!", this);
                enabled = false;
                return;
            }
        }
    }

    void LateUpdate()   // ← better for visual smoothing / camera follow
    {
        if (target == null) return;

        // Option A: Simple teleport (good if target is kinematic / no physics conflict)
        transform.position = target.position;

        // Option B: If this object has Rigidbody2D → use MovePosition (physics-friendly)
        // if (myRb != null)
        // {
        //     myRb.MovePosition(target.position);
        // }
        // else
        // {
        //     transform.position = target.position;
        // }

        // Optional: also copy rotation if needed
        // transform.rotation = target.rotation;
    }
}