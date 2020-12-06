using UnityEngine;

///<summary>
/// A utility ScriptableObject that can be created as an asset to be used as a Debug.Log() method without opening a code editor.
/// For example, this asset can be useful for logging informations from UnityEvents.
///</summary>
[CreateAssetMenu(fileName = "NewLogger", menuName = "Utility/Logger")]
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/logger.md")]
public class Logger : ScriptableObject
{

    #region Properties

    [SerializeField]
    [Tooltip("The eventual prefix to add to a message that contains a value.")]
    private string m_Prefix = string.Empty;

    [SerializeField]
    [Tooltip("The eventual suffix to add to a message that contains a value.")]
    private string m_Suffix = string.Empty;

    #endregion


    #region Static Log Methods

    /// <summary>
    /// Logs the given message. Note that Prefix and Suffix are not used in that case.
    /// </summary>
    public void LogMessage(string _Message)
    {
        Debug.Log(_Message);
    }

    #endregion


    #region Dynamic Log Methods

    /// <summary>
    /// Logs the defined prefix and suffix.
    /// </summary>
    public void Log()
    {
        Debug.Log($"{m_Prefix} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(string _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(int _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(float _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(bool _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(Vector3 _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(Quaternion _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    /// <summary>
    /// Logs the given information, adding the defined prefix and suffix.
    /// </summary>
    public void Log(GameObject _Data)
    {
        Debug.Log($"{m_Prefix} {_Data} {m_Suffix}");
    }

    #endregion

}