using UnityEngine;
using System.Collections.Generic;

public class NavigationManager : MonoBehaviour
{
    public GameObject dashboardPanel;
    public GameObject userPanel;
    public GameObject servicePanel;

    public void ShowPanel(string panelName)
    {
        dashboardPanel.SetActive(panelName == "dashboard");
        userPanel.SetActive(panelName == "user");
        servicePanel.SetActive(panelName == "service");
    }
}
