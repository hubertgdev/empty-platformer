using UnityEngine;

///<summary>
/// 
///</summary>
public class PatrollerController : MonoBehaviour
{

    public enum EPatrollerPathDirection
    {
        Left,
        Right
    }

    [SerializeField]
    private float m_Speed = 3f;

    [SerializeField]
    private EPatrollerPathDirection m_PathDirection = EPatrollerPathDirection.Right;

    [SerializeField]
    private float m_Distance = 9f;

    [SerializeField]
    private BoxCollider m_Collider = null;

    private Vector3 m_Origin = Vector3.zero;
    private float m_CurrentPathDistance = 0f;
    private bool m_Forward = true;

    private void Awake()
    {
        if(m_Collider == null) { m_Collider = GetComponent<BoxCollider>(); }
        m_Origin = transform.position;
    }

    private void Update()
    {
        UpdatePosition(Time.deltaTime);
    }

    private void UpdatePosition(float _DeltaTime)
    {
        float movement = m_Speed * _DeltaTime;
        m_CurrentPathDistance = m_Forward ? Mathf.Min(m_CurrentPathDistance + movement, m_Distance) : Mathf.Max(0f, m_CurrentPathDistance - movement);
        if(m_CurrentPathDistance == m_Distance || m_CurrentPathDistance == 0f)
        {
            m_Forward = !m_Forward;
        }

        transform.position = m_Origin + ForwardVector * m_CurrentPathDistance;
    }

    private Vector3 Size
    {
        get
        {
            if(m_Collider == null) { m_Collider = GetComponent<BoxCollider>(); }
            return m_Collider != null ? m_Collider.bounds.size : Vector3.one;
        }
    }

    private Vector3 ForwardVector
    {
        get { return (m_PathDirection == EPatrollerPathDirection.Right) ? Vector3.right : Vector3.left; }
    }

    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        // Draw patrol path
        Gizmos.color = Color.red;
        Vector3 size = Size;
        size.x += m_Distance;
        Vector3 origin = (UnityEditor.EditorApplication.isPlaying) ? m_Origin : transform.position;
        Vector3 center = origin - ForwardVector * (Size.x / 2f) + ForwardVector * (size.x / 2f);
        Gizmos.DrawWireCube(center, size);
    }

    #endif

}