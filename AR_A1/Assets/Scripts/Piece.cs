using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    PlaceObjects placeObjects;
    bool isSelected = false;
    float rotationIncrements = 45f;

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

    public void RotatePiece(string directionString)
    {
        int direction = 0;
        if(directionString == "left")
        {
            direction = -1;
        }
        else if(directionString == "right")
        {
            direction = 1;
        }
        float rotationY = transform.rotation.y + (rotationIncrements * direction);
        transform.Rotate(new Vector3(0, rotationY, 0));
    }

    public void DestroyPiece()
    {
        Destroy(gameObject);
        placeObjects.target = null;
    }

    
}
