using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseObject : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;

    PlaceObjects placeObjects;

    private void Start()
    {
        placeObjects = FindObjectOfType<PlaceObjects>();
    }

    public void PressObjectButton()
    {
        placeObjects.target = objectPrefab.transform;
        placeObjects.state = PlaceObjects.States.spawn;
    }

}
