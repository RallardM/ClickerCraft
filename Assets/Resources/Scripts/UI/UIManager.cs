
using UnityEngine;
using static IngredientManager;

public class UIManager : MonoBehaviour
{
    private static UIManager _Instance;

    private static Sprite m_fireSprite;
    private static Sprite m_airSprite;
    private static Sprite m_earthSprite;
    private static Sprite m_waterSprite;
    private static Sprite m_etherSprite;
    private static Sprite m_blazeSprite;
    private static Sprite m_coalSprite;
    private static Sprite m_vaporSprite;
    private static Sprite m_fireburstSprite;
    private static Sprite m_archeaSprite;
    private static Sprite m_magmaSprite;
    private static Sprite m_rockSprite;
    private static Sprite m_mudSprite;
    private static Sprite m_DustSprite;
    private static Sprite m_seedSprite;
    private static Sprite m_steamSprite;
    private static Sprite m_pondSprite;
    private static Sprite m_puddleSprite;
    private static Sprite m_rainSprite;
    private static Sprite m_algeaSprite;
    private static Sprite m_fireboltSprite;
    private static Sprite m_duststormSprite;
    private static Sprite m_thunderstormSprite;
    private static Sprite m_tornadoSprite;
    private static Sprite m_fungiSprite;
    private static Sprite m_pyroidSprite;
    private static Sprite m_golemSprite;
    private static Sprite m_undineSprite;
    private static Sprite m_sylphSprite;
    private static Sprite m_spiritSprite;

    private void Awake()
    {
        m_fireSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_0");
        m_airSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_1");
        m_earthSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_2");
        m_waterSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_3");
        m_etherSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_4");
        m_blazeSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_5");
        m_coalSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_6");
        m_vaporSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_7");
        m_fireburstSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_8");
        m_archeaSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_9");
        m_magmaSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_10");
        m_rockSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_11");
        m_mudSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_12");
        m_DustSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_13");
        m_seedSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_14");
        m_steamSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_15");
        m_pondSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_16");
        m_puddleSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_17");
        m_rainSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_18");
        m_algeaSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_19");
        m_fireboltSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_20");
        m_duststormSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_21");
        m_thunderstormSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_22");
        m_tornadoSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_23");
        m_fungiSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_24");
        m_pyroidSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_25");
        m_golemSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_26");
        m_undineSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_27");
        m_sylphSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_28");
        m_spiritSprite = Resources.Load<Sprite>("Assets/Externals/Reddit/Mix-elements_29");
    }

    public static UIManager GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new UIManager();
        }

        return _Instance;
    }

    public UIManager()
    {
        if (_Instance)
        {
            Destroy(this);
            return;
        }
    }
}
