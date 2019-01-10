using UnityEngine;

public class Tips : MonoBehaviour
{
    private bool firstTime = true;

    public static Tips Instance;

    public GameObject tips;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SecurePlayerPrefs.Init();



        CheckForTips();
    }

    // TapToPlay isActive
    public void CheckForTips()
    {
        if (!SecurePlayerPrefs.GetBool("NotFirstTime"))
        {
            tips.gameObject.SetActive(true);
            
            Debug.Log("Tip");
            SecurePlayerPrefs.SetBool("NotFirstTime", true);
        }
        else
        {
            tips.gameObject.SetActive(false);
            Debug.Log("No TIP");

        }
    }

    public void DesactivateTips()
    {
        tips.gameObject.SetActive(false);
    }
}
