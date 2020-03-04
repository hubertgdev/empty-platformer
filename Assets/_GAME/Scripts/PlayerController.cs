using UnityEngine;

///<summary>
/// 
///</summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField]
    private float m_JumpForce = 12f;

    [Header("References")]

    [SerializeField]
    private Movement m_Movement = null;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    private void Awake()
    {
        if(m_Movement == null) { m_Movement = GetComponent<Movement>(); }
        if(m_Rigidbody == null) { m_Rigidbody = GetComponent<Rigidbody>(); }
    }

	private void Update()
    {
        CheckMovement();
        CheckJump();
    }

    private void CheckMovement()
    {
        float hMovement = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * hMovement;

        m_Movement.Move(movement, Time.deltaTime);
    }

    private void CheckJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }
    }

}