using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [Header("UI References")]
    public Image[] tabImages; 
    public GameObject[] pages;

    [Header("Settings")]
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    void Start()
    {
        if (pages.Length > 0 && tabImages.Length > 0)
        {
            ActivateTab(0);
        }
    }

    public void ActivateTab(int tabNo)
    {
        int count = Mathf.Min(pages.Length, tabImages.Length);

        for (int i = 0; i < count; i++)
        {
            if (pages[i] != null) 
                pages[i].SetActive(false);

            if (tabImages[i] != null) 
                tabImages[i].color = inactiveColor;
        }

        if (tabNo >= 0 && tabNo < count)
        {
            if (pages[tabNo] != null) 
                pages[tabNo].SetActive(true);
            
            if (tabImages[tabNo] != null) 
                tabImages[tabNo].color = activeColor;
        }
    }
}