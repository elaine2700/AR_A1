using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool horizontal = true; // horizontal or vertical placement
    PlaceObjects placeObjects;
    float rotationIncrements = 45f;
    string verticalLayer = "Vertical";
    string horizontalLayer = "Horizontal";

    private void Start()
    {
        placeObjects = FindObjectOfType<PlaceObjects>();
        //verticalLayer = LayerMask.NameToLayer("Vertical");
        Debug.Log("Vertical layer: " + verticalLayer);
        //horizontalLayer = LayerMask.NameToLayer("Horizontal");
        Debug.Log("Horizontal layer: " + horizontalLayer);
    }

    public void EditPiece(Canvas menu)
    {
        menu.gameObject.SetActive(true);
        placeObjects.state = PlaceObjects.States.edit;
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

    public void MovePiece(Vector3 newPos, string Alignment)
    {
        Debug.Log("Collider alignment: " + Alignment + "vs Piece Alignment: " + horizontal);
        if(Alignment == horizontalLayer && horizontal)
        {
            transform.position = newPos;
        }
        else if ( Alignment == verticalLayer && !horizontal)
        {
            transform.position = newPos;
        }
        else
        {
            Debug.Log("pick another position");
        }
    }
    
}
