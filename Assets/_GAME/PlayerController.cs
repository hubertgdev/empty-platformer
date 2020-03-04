using UnityEngine;

///<summary>
/// 
///</summary>
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Movement m_Movement = null;

    private void Awake()
    {
        if(m_Movement == null) { m_Movement = GetComponent<Movement>(); }
    }

	private void Update()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        float hMovement = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * hMovement;

        m_Movement.Move(movement, Time.deltaTime);
    }

}