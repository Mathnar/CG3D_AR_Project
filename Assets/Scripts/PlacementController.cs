using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
 
public class PlacementController : MonoBehaviour
{
    [SerializeField]
    public GameObject placePrefab;

    private GameObject spawnPrefab;
    private ARRaycastManager arRaycastmanager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        arRaycastmanager = GetComponent<ARRaycastManager>();

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            return true;

        }

        touchPosition = default;
        return false;

    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (arRaycastmanager.Raycast(touchPosition, hits, trackableTypes:TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnPrefab == null)
                spawnPrefab = Instantiate(placePrefab, hitPose.position, hitPose.rotation);
            else
                spawnPrefab.transform.position = hitPose.position;
            
        }
    }

}