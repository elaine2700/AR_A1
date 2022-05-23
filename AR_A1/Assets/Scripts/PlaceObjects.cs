using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    [SerializeField] Canvas editionMenu;

    public Transform target;
    
    public enum States { edit, spawn};
    public States state = States.spawn;

    private void Update()
    {
        bool isPressing = false;
        Vector3 pressPosition = Vector3.zero;

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
 
            pressPosition = touch.position;
            isPressing = true;
        }
#endif

        // This *will* work on mobile, but gives you the *average* touch, which
        // may not be desirable (and will not allow for multi-touch).
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            pressPosition = Input.mousePosition;
            isPressing = true;
        }
#endif

        if (isPressing)
        {
            Ray ray = Camera.main.ScreenPointToRay(pressPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // selecting piece to edit
                if(state == States.edit)
                {
                    if(hit.collider.gameObject.GetComponent<Piece>())
                    {
                        target = hit.collider.transform;
                        target.GetComponent<Piece>().EditPiece(editionMenu);
                        
                    }
                    // select the piece to change its position.
                    else if(target != null)
                    {
                        MoveTarget(hit.point, hit.transform.tag);
                    }
                }
                else
                {
                    // choose an object from the UI to start spawning
                    if (target != null && state == States.spawn)
                        SpawnPiece(hit.point, hit.transform.tag);
                    state = States.edit;
                }
            }
        }
    }

    public void DeselectPiece()
    {
        editionMenu.gameObject.SetActive(false);
        target = null;
    }

    public void RotateLeft()
    {
        target.GetComponent<Piece>().RotatePiece("left");
    }

    public void RotateRight()
    {
        target.GetComponent<Piece>().RotatePiece("right");
    }

    public void DestroyTarget()
    {
        target.GetComponent<Piece>().DestroyPiece();
        editionMenu.gameObject.SetActive(false);
    }

    private void MoveTarget(Vector3 newPosition, string alignment)
    {
        target.GetComponent<Piece>().MovePiece(newPosition, alignment);
    }

    private void SpawnPiece(Vector3 spawnPos, string alignment)
    {
        bool pieceAlignment = target.GetComponent<Piece>().horizontal;
        if (pieceAlignment && alignment == "Horizontal")
            Instantiate(target.gameObject, spawnPos, Quaternion.identity);
        else if (!pieceAlignment && alignment == "Vertical")
            Instantiate(target.gameObject, spawnPos, Quaternion.identity);
        else
            Debug.Log("Pick another position");
    }
}
