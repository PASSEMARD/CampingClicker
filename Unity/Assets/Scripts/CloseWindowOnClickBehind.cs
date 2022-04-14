using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseWindowOnClickBehind : MonoBehaviour, IPointerDownHandler
{
    [field: SerializeField] public GameObject windowToClose { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        windowToClose.SetActive(false);
    }
}
