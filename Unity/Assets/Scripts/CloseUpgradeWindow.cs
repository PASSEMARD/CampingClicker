using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUpgradeWindow : MonoBehaviour, IPointerDownHandler
{
    public GameObject windowToClose;

    public void OnPointerDown(PointerEventData eventData)
    {
        windowToClose.SetActive(false);
    }
}
