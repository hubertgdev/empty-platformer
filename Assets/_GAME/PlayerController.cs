using UnityEngine;

///<summary>
/// 
///</summary>
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Movement m_Movement = null;

    // [SerializeField]
    // private float m_GravityScale = 1f;

    // private float m_YVelocity = 0f;

    private void Awake()
    {
        if(m_Movement == null) { m_Movement = GetComponent<Movement>(); }
    }

	private void Update()
    {
        CheckMovement();

        // Apply gravity
        // m_YVelocity += Time.deltaTime * Physics.gravity.y;
        // transform.position += Vector3.up * m_YVelocity * Time.deltaTime;
    }

    private void CheckMovement()
    {
        float hMovement = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * hMovement;

        m_Movement.Move(movement, Time.deltaTime);
    }

}