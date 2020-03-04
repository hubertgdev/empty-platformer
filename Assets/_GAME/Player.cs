using UnityEngine;

///<summary>
/// Represents a Player, with movement and jump controls.
///</summary>
public class Player : MonoBehaviour
{

    [Header("Settings")]

    [SerializeField]
    private float m_Speed = 200f;

    [SerializeField]
    private float m_JumpForce = 12f;

    [SerializeField, Tooltip("Defines the physics layers to detect when checking if player is on the floor")]
    private LayerMask m_IsOnFloorDetectionLayer = ~0;

    [SerializeField]
    private float m_IsOnFloorDetectionDefaultOffset = 1.1f;

    [Header("References")]

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    /// <summary>
    /// Called when the scene is loaded.
    /// </summary>
    private void Awake()
    {
        // Initialize the Rigidbody reference if it's not already set.
        if(m_Rigidbody == null)
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        CheckMovement();
        CheckJump();
    }

    /// <summary>
    /// Checks movement inputs, and apply movement if required.
    /// </summary>
    private void CheckMovement()
    {
        float movement = Input.GetAxis("Horizontal");

        Vector3 velocity = m_Rigidbody.velocity;
        velocity.x = movement * m_Speed * Time.deltaTime;

        m_Rigidbody.velocity = velocity;
    }

    /// <summary>
    /// Checks jump inputs, and apply jump if required and possible.
    /// </summary>
    private void CheckJump()
    {
        Debug.Log("Is on floor: " + IsOnFloor());
        if(Input.GetButton("Jump") && IsOnFloor())
        {
            Debug.Log("Ok");
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Checks if the player is on the floor.
    /// </summary>
    private bool IsOnFloor()
    {
        float rayDistance = (m_Rigidbody.velocity.y < 0f) ? m_Rigidbody.velocity.y : m_IsOnFloorDetectionDefaultOffset;
        if(Physics.Raycast(transform.position, Vector3.down, rayDistance, m_IsOnFloorDetectionLayer))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Draws gizmos in scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        // Draw the "Is on floor" detection raycast
        Rigidbody rb = (m_Rigidbody == null) ? GetComponent<Rigidbody>() : m_Rigidbody;
        if(rb != null)
        {
            float rayDistance = (rb.velocity.y < 0f) ? rb.velocity.y : m_IsOnFloorDetectionDefaultOffset;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
        }
    }

}