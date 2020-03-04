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

	[SerializeField]
    private float m_Speed = 8f;

    [SerializeField]
    private EChangeOrientationAxis m_ChangeOrientation = EChangeOrientationAxis.TransformRight;

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
    
    public bool Move(Vector3 _Direction, float _DeltaTime)
    {
        if(_Direction == Vector3.zero)
        {
            if(m_MovedLastFrame)
            {
                m_MovedLastFrame = false;
                m_OnStopMove.Invoke();
            }

            return false;
        }

        if(!m_MovedLastFrame)
        {
            m_MovedLastFrame = true;
            m_OnBeginMove.Invoke();
        }

        Vector3 lastPosition = transform.position;

        Vector3 targetPosition = transform.position + _Direction * m_Speed * _DeltaTime;
        transform.position = targetPosition;

        ChangeOrientation(_Direction);
        m_OnUpdateMove.Invoke(new MovementInfos { speed = m_Speed, lastPosition = lastPosition, currentPosition = targetPosition });

        return true;
    }

    private void ChangeOrientation(Vector3 _Direction)
    {
        Vector3 orientation = _Direction;
        orientation.Normalize();

        if(orientation == m_LastOrientation)
        {
            return;
        }

        switch(m_ChangeOrientation)
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

}