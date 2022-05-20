using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    PlaceObjects placeObjects;
    bool isSelected = false;

    private void Start()
    {
        placeObjects = FindObjectOfType<PlaceObjects>();
    }

    public void EditPiece(Canvas menu)
    {
        isSelected = true;
        menu.gameObject.SetActive(true);
        placeObjects.state = PlaceObjects.States.select;
        Debug.Log("editing piece");
    }

    
}
