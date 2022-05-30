using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] Color lockedColor = Color.red;
    [SerializeField] Color unlockedColor = Color.white;
    [SerializeField] RectTransform moveButton;

    private void Start()
    {
        ChangeMoveButton(true);
    }

    public void ChangeMoveButton(bool locked)
    {
        Color newColor = Color.white;
        if (locked)
        {
            newColor = lockedColor;
        }
        else
        {
            newColor = unlockedColor;
        }
        moveButton.gameObject.GetComponent<Image>().color = newColor;
        
    }

    
}
