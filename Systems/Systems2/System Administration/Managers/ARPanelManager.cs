using UnityEngine;

public class ARPanelManager : MonoBehaviour
{
    public GameObject adminPanelPrefab;

    void Start()
    {
        // Instantiate AR Panel in the scene
        GameObject panel = Instantiate(adminPanelPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        panel.transform.SetParent(Camera.main.transform); // Attach to AR camera for tracking
    }
}
