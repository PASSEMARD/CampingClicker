using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace clicker
{
    public class LoadButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject windowToClose;

        [SerializeField] private GameObject loadingScreen;

        [field: SerializeField] public MainNetworkObject Network { get; private set; }
        [field: SerializeField] public Button CustomBehavior { get; private set; }


        private void Start()
        {
            CustomBehavior.onClick.AddListener(CustomBehavior_OnClick);
        }


        private void CustomBehavior_OnClick()
        {
            // Load the game if the code have a good size
            if(inputField.text.Length == 6)
            {
                // Close the window 
                windowToClose.SetActive(false);

                // Start the loading screen
                loadingScreen.SetActive(true);

                // Send the data
                Network.Load(SuccessHandler, ErrorHandler, inputField.text);
            }
        }

        private void ErrorHandler(string error, string text)
        {
            Debug.Log(error);
            Debug.Log(text);
        }


        /// <summary>
        /// Class use to regroup the loading information from the server
        /// </summary>
        [System.Serializable]
        private class LoadJsonResponse
        {
            public int score;
            public int upgradeClick;
            public int upgradeGatherer;
        }
        
        /// <summary>
        /// Handle the success of the server answer from loading
        /// </summary>
        /// <param name="res">the JSON value for the save to load</param>
        private void SuccessHandler(string res)
        {
            // Close the loading screen
            loadingScreen.SetActive(false);

            // Read Json received from the server
            LoadJsonResponse rep = JsonUtility.FromJson<LoadJsonResponse>(res);
            Debug.Log(rep.score);
            Debug.Log(rep.upgradeClick);
            Debug.Log(rep.upgradeGatherer);
        }
    }
}
