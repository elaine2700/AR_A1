using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    //[SerializeField] GameObject furniture;
    //[SerializeField] GameObject proxyParent;
    [SerializeField] Canvas editionMenu;

    public Transform target;
    
    public enum States { select, spawn};
    public States state = States.select;

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
                if(target == null && state == States.select)
                {
                    if(hit.collider.gameObject.GetComponent<Piece>())
                    {
                        Debug.Log("found piece");
                        target = hit.collider.transform;
                        target.GetComponent<Piece>().EditPiece(editionMenu);
                    }
                }
                else
                {
                    //spawning
                    if(target != null && state == States.spawn)
                        Instantiate(target.gameObject, hit.point, Quaternion.identity);
                    state = States.select;
                    target = null;
                }
            }
        }
    }

    public void DeselectPiece()
    {
        editionMenu.gameObject.SetActive(false);
        target = null;
    }
    /*public void PlaceOnWorld()
    {
        Debug.Log("Furniture Selected");
        GameObject newObject = Instantiate(furniture);
        newObject.transform.parent = proxyParent.transform;
    }*/
}
