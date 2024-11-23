using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
public class ARPlacement : MonoBehaviour
{
    public GameObject panelPrefab;
    private ARRaycastManager raycastManager;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits))
                {
                    Pose hitPose = hits[0].pose;
                    Instantiate(panelPrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
