using UnityEngine;
using UnityEngine.Events;

///<summary>
/// 
///</summary>
public class PlatformerController : MonoBehaviour
{

    #region Properties

    [Header("Movement Settings")]

    [SerializeField, Tooltip("Maximum speed of the character")]
    private float m_Speed = 8f;

    [SerializeField, Tooltip("Defines the layers to check when detecting obstacles")]
    private LayerMask m_ObstaclesDetectionLayer = ~0;

    [SerializeField, Tooltip("By default, use the Collider on this GameObject")]
    private BoxCollider m_Collider = null;

    [Header("Movement Events")]

    // Called when the character starts moving
    [SerializeField]
    private MovementInfosEvent m_OnBeginMove = new MovementInfosEvent();

    // Called each frame the character is moving (even if there's an obstacle in front of it)
    [SerializeField]
    private MovementInfosEvent m_OnUpdateMove = new MovementInfosEvent();

    // Called when the character stops moving
    [SerializeField]
    private UnityEvent m_OnStopMove = new UnityEvent();

    // Called when the character changes its movment direction
    [SerializeField]
    private Vector3Event m_OnChangeOrientation = new Vector3Event();

    // Stores the last orientation of the character to eventually trigger the OnChangeOrientation event if it changes
    private Vector3 m_LastOrientation = Vector3.zero;

    // Defines if the player has moved the last frame or not, in order to trigger OnBegin|Update|StopMove events
    private bool m_MovedLastFrame = false;

    // Debug properties

    #if UNITY_EDITOR

    // Store the last "cast distance" of the obstacles detection, for drawing gizmos
    private float m_LastObstaclesDetectionCastDistance = 0f;

    #endif

    #endregion


    #region Lifecycle

    /// <summary>
    /// Called when this component is loaded.
    /// </summary>
    private void Awake()
    {
        if (m_Collider == null) { m_Collider = GetComponent<BoxCollider>(); }
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        CheckMovement(Time.deltaTime);
    }

    #endregion


    #region Movement

    /// <summary>
    /// Checks for the movement inputs, and apply movement if required.
    /// </summary>
    private void CheckMovement(float _DeltaTime)
    {
        // Get the movement input
        float hMovement = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * hMovement;

        // Apply movement
        Move(movement, _DeltaTime);
    }

    /// <summary>
    /// Moves the character, based on the given direction.
    /// </summary>
    /// <param name="_Direction">The movement direciton (clamped to 1).</param>
    /// <returns>Returns true if the movement can be resolved, otherwise false. Note that the movement is resolved event if an obstacle is
    /// on the way.</returns>
    private bool Move(Vector3 _Direction, float _DeltaTime)
    {
        // Clamps the direction, so the character can move faster than its maximum speed, but it can move slower.
        _Direction = Vector3.ClampMagnitude(_Direction, 1f);

        // If the input direction is null
        if (_Direction == Vector3.zero)
        {
            // If the player moved the last frame
            if (m_MovedLastFrame)
            {
                // Call onStopMove event
                m_MovedLastFrame = false;
                m_OnStopMove.Invoke();
            }

            // The movement hasn't been applied: return false
            Debug.Log("Didn't move");
            return false;
        }

        Vector3 lastPosition = transform.position;
        Vector3 targetPosition = lastPosition;

        // If player didn't move last frame
        if (!m_MovedLastFrame)
        {
            // Call OnBeginMove event
            m_MovedLastFrame = true;
            m_OnBeginMove.Invoke(new MovementInfos { speed = m_Speed, lastPosition = lastPosition, currentPosition = targetPosition });
        }

        // Process obstacles detection

        float castDistance = m_Speed * Mathf.Abs(_Direction.x) * Time.deltaTime;
        #if UNITY_EDITOR
        m_LastObstaclesDetectionCastDistance = castDistance;
        #endif
        // If no obstacles hit
        if (!Physics.BoxCast(transform.position, Extents, transform.right, Quaternion.identity, castDistance, m_ObstaclesDetectionLayer))
        {
            // Change target position
            targetPosition = transform.position + _Direction * m_Speed * _DeltaTime;
        }

        // Set the new player position and orientation
        transform.position = targetPosition;
        Orientation = _Direction;
        // Call OnUpdateMove event
        m_OnUpdateMove.Invoke(new MovementInfos { speed = m_Speed, lastPosition = lastPosition, currentPosition = targetPosition });

        // The movement has been applied: return true
        Debug.Log("Moved");
        return true;
    }

    /// <summary>
    /// Gets the current orientation of the character.
    /// While the game is a Platformer 2D, its forward vector (its orientation) is transform.right.
    /// 
    /// Sets the orientation of the character. If the orientation is different, call OnChangeOrientation event.
    /// </summary>
    private Vector3 Orientation
    {
        get { return transform.right; }
        set
        {
            // Normalize the given direction
            Vector3 orientation = value;
            orientation.Normalize();

            // If the new orientation is equal to the current one, stop
            if (orientation == m_LastOrientation) { return; }

            // Apply orientation and call OnChangeOrientation event
            transform.right = orientation;
            m_OnChangeOrientation.Invoke(orientation);
            m_LastOrientation = orientation;
        }
    }

    #endregion


    #region Common

    /// <summary>
    /// Returns the size of the object, based on its collider's bounds.
    /// If no collider set, returns Vector3.one.
    /// </summary>
    private Vector3 Size
    {
        get
        {
            if (m_Collider != null)
            {
                return m_Collider.bounds.size;
            }
            return Vector3.one;
        }
    }

    /// <summary>
    /// Returns the extents (half the size) of the object, based on it's collider bounds.
    /// If no collider set, returns Vector3.one / 2.
    /// </summary>
    private Vector3 Extents
    {
        get
        {
            if (m_Collider != null)
            {
                return m_Collider.bounds.extents;
            }
            return Vector3.one / 2;
        }
    }

    #endregion


    #region Debug & Tests

    #if UNITY_EDITOR

    /// <summary>
    /// Draws Gizmos for this component in the Scene View.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Draw the obstacles detection cast
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Orientation * m_LastObstaclesDetectionCastDistance, Size);
    }

    #endif

    #endregion

}