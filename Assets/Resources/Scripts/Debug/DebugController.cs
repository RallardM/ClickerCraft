using UnityEngine;

public class DebugController : MonoBehaviour
{
    private static DebugController _Instance;
    private Transform m_debugMenuCanvas;
    static public bool m_isInDebugMode = false;

    public static DebugController GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new DebugController();
        }

        return _Instance;
    }

    public DebugController()
    {
        if (_Instance)
        {
            Destroy(this);
            return;
        }
    }

    private void Awake()
    {
        _Instance = this;
        m_debugMenuCanvas = transform.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        InputHandle();
        RenderDebugMenu();
    }

    private void RenderDebugMenu()
    {
        m_debugMenuCanvas.gameObject.SetActive(m_isInDebugMode);
    }

    private void InputHandle()
    {
        if (Input.GetKeyDown("f1"))
        {
            m_isInDebugMode = !m_isInDebugMode;
        }
    }
}
