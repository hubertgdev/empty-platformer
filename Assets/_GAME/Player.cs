using System.Collections;

using UnityEngine;

///<summary>
/// Represents a Player, with movement, jump and shoot controls.
///</summary>
public class Player : MonoBehaviour
{

    [Header("Movement Settings")]

    [SerializeField]
    private float m_Speed = 200f;

    [Header("Jump Settings")]

    [SerializeField]
    private float m_JumpForce = 12f;

    [SerializeField, Tooltip("Defines the physics layers to detect when checking if player is on the floor")]
    private LayerMask m_IsOnFloorDetectionLayer = ~0;

    [SerializeField]
    private float m_IsOnFloorDetectionOffset = 1.1f;

    [Header("Shoot Settings")]

    [SerializeField]
    private float m_ShootCooldown = 0.2f;

    [SerializeField]
    private float m_ShootRange = 8f;

    [Header("References")]

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    // Flow

    private float m_ShootCooldownTimer = 0f;

    /// <summary>
    /// Called when the scene is loaded.
    /// </summary>
    private void Start()
    {
        // Initialize the Rigidbody reference if it's not already set.
        if(m_Rigidbody == null)
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        m_ShootCooldownTimer = m_ShootCooldown;
        StartCoroutine(UpdateShootCooldown(m_ShootCooldown));
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        CheckMovement();
        CheckJump();
        CheckShoot();
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
        if(velocity.x != 0f)
        {
            transform.right = Vector3.right * Mathf.Sign(velocity.x);
        }
    }

    /// <summary>
    /// Checks jump inputs, and apply jump if required and possible.
    /// </summary>
    private void CheckJump()
    {
        if(Input.GetButton("Jump") && IsOnFloor())
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Checks shoot input, and do shoot action if required and possible.
    /// </summary>
    private void CheckShoot()
    {
        if(Input.GetButton("Shoot") && CanShoot())
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * m_ShootRange, Color.red, m_ShootCooldown * 6);
            m_ShootCooldownTimer = 0f;
        }
    }

    /// <summary>
    /// Checks if the player is on the floor.
    /// </summary>
    private bool IsOnFloor()
    {
        if(Physics.Raycast(transform.position, Vector3.down, m_IsOnFloorDetectionOffset, m_IsOnFloorDetectionLayer))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the player can shoot.
    /// In fact, checks if the "shoot timer" is more or equal to "shoot cooldown".
    /// </summary>
    private bool CanShoot()
    {
        return m_ShootCooldownTimer >= m_ShootCooldown;
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
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_IsOnFloorDetectionOffset);
        }
    }

    /// <summary>
    /// Persistent coroutine that updates the shoot timer if needed.
    /// Use CanShoo() to check if the cooldown is elapsed or not.
    /// </summary>
    private IEnumerator UpdateShootCooldown(float _Cooldown)
    {
        while(true)
        {
            if(m_ShootCooldownTimer < _Cooldown)
            {
                m_ShootCooldownTimer += Time.deltaTime;
            }

            yield return null;
        }
    }

}