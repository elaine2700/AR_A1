using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField] ARRaycastManager RaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField] GameObject spawnablePrefab;

    Camera arCam;
    GameObject spawnedObject;

    private void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if(RaycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(hits[0].pose.position);
                    }
                }
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                spawnedObject.transform.position = hits[0].pose.position;
            }

            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
    }
}
