using UnityEngine;
using UnityEngine.Events;

public enum EChangeOrientationAxis
{
    None,
    TransformRight,
    TransformForward
}

///<summary>
/// Allow a character to move.
///</summary>
public class Movement : MonoBehaviour
{

    [Header("Settings")]

    [SerializeField]
    private float m_Speed = 8f;

    [SerializeField]
    private EChangeOrientationAxis m_OrientationVector = EChangeOrientationAxis.TransformRight;

    [SerializeField]
    private LayerMask m_ObstaclesDetection = ~0;

    [SerializeField]
    private BoxCollider m_Collider = null;

    [Header("Events")]

    [SerializeField]
    private UnityEvent m_OnBeginMove = new UnityEvent();

    [SerializeField]
    private MovementInfosEvent m_OnUpdateMove = new MovementInfosEvent();

    [SerializeField]
    private UnityEvent m_OnStopMove = new UnityEvent();

    [SerializeField]
    private Vector3Event m_OnChangeOrientation = new Vector3Event();

    private Vector3 m_LastOrientation = Vector3.zero;
    private bool m_MovedLastFrame = false;

    private void Awake()
    {
        if (m_Collider == null) { m_Collider = GetComponent<BoxCollider>(); }
    }

    public bool Move(Vector3 _Direction, float _DeltaTime)
    {
        if (_Direction == Vector3.zero)
        {
            if (m_MovedLastFrame)
            {
                m_MovedLastFrame = false;
                m_OnStopMove.Invoke();
            }

            return false;
        }

        if (!m_MovedLastFrame)
        {
            m_MovedLastFrame = true;
            m_OnBeginMove.Invoke();
        }

        Vector3 lastPosition = transform.position;
        Vector3 targetPosition = lastPosition;

        float castDistance = m_Speed * Mathf.Abs(_Direction.x) * Time.deltaTime;
        // If no obstacles hit
        if (!Physics.BoxCast(transform.position, Extents, Orientation, Quaternion.identity, castDistance, m_ObstaclesDetection))
        {
            // Change target position
            targetPosition = transform.position + _Direction * m_Speed * _DeltaTime;
        }

        transform.position = targetPosition;
        ChangeOrientation(_Direction);
        m_OnUpdateMove.Invoke(new MovementInfos { speed = m_Speed, lastPosition = lastPosition, currentPosition = targetPosition });

        return true;
    }

    private void ChangeOrientation(Vector3 _Direction)
    {
        Vector3 orientation = _Direction;
        orientation.Normalize();

        if (orientation == m_LastOrientation)
        {
            return;
        }

        switch (m_OrientationVector)
        {
            case EChangeOrientationAxis.TransformForward:
                transform.forward = orientation;
                break;

            case EChangeOrientationAxis.TransformRight:
                transform.right = orientation;
                break;

            default:
                break;
        }

        m_OnChangeOrientation.Invoke(orientation);
        m_LastOrientation = orientation;
    }

    private Vector3 Orientation
    {
        get
        {
            Vector3 orientation = transform.forward;
            switch (m_OrientationVector)
            {
                case EChangeOrientationAxis.TransformRight:
                    orientation = transform.right;
                    break;

                default:
                    break;
            }
            return orientation;
        }


    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position + Orientation * m_Speed * Time.deltaTime, Size);
    }

}