using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    //[SerializeField] GameObject furniture;
    //[SerializeField] GameObject proxyParent;

    public Transform target;

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
                target.position = hit.point;
            }
        }
    }

    /*public void PlaceOnWorld()
    {
        Debug.Log("Furniture Selected");
        GameObject newObject = Instantiate(furniture);
        newObject.transform.parent = proxyParent.transform;
    }*/
}
