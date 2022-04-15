using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace clicker
{
    public class ShowSimpleInformation : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private GameObject windowToShow;

        public void Show(string textToShow, float size)
        {
            tmpText.fontSize = size;
            tmpText.text = textToShow;

            // Show window
            windowToShow.SetActive(true);
        }
    }
}
