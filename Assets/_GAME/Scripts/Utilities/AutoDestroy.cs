using UnityEngine;

///<summary>
/// Destorys the attached GameObject after a defined delay. This is useful to destroy instantiated particles effects after their emission
/// for example.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/auto-destroy.md")]
public class AutoDestroy : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Defines the time (in seconds) before this GameObject is destroyed.")]
    private float m_DestroyDelay = 3f;

    private void Start()
    {
        Destroy(gameObject, Mathf.Max(m_DestroyDelay, 0f));
    }

}