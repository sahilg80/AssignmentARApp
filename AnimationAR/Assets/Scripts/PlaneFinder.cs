using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
//[RequireComponent(typeof(ARRaycastManager))]
public class PlaneFinder : MonoBehaviour
{
    public GameObject placementIndicator;
    private bool placementPosIsValid;
    public static Pose placementPose;
    private ARRaycastManager aRRaycastManager;

    private GameObject statusText;

    public static bool isGameStarted;
   
    Vector2 touchPos;
   
    void Start()
    {
        statusText = GameObject.FindGameObjectWithTag("StatusText");
       
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isGameStarted)
        {
            UpdatePlacementIndicator();
            UpdatePlacementPose();
        }

        if (ScreenIsTouched(out Vector2 touchPos) && placementPosIsValid && !isGameStarted)
        {
            
            placementIndicator.SetActive(false);
           
            isGameStarted = true;
            statusText.GetComponent<TextMeshProUGUI>().SetText("");
        }

    }

    private void UpdatePlacementIndicator()
    {
        if (placementPosIsValid)
        {
            
            statusText.GetComponent<TextMeshProUGUI>().SetText("Tap on Screen");
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            
            statusText.GetComponent<TextMeshProUGUI>().SetText("Searching Plane ...");
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPosIsValid = hits.Count > 0;
        if (placementPosIsValid)
        {
            placementPose = hits[0].pose;
        }

    }
    bool ScreenIsTouched(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }

    
}
