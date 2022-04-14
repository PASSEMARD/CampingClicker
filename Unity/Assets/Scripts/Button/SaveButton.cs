using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace clicker
{
    public class SaveButton : MonoBehaviour
    {

        [SerializeField] private Button customBehavior;
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private MainNetworkObject network;

        // Start is called before the first frame update
        void Start()
        {
            customBehavior.onClick.AddListener(CustomBehavior_OnClick);
        }


        /// <summary>
        /// Change the behavior of the Button
        /// </summary>
        private void CustomBehavior_OnClick()
        {
            // Launch the loading screen
            loadingScreen.SetActive(true);

            // Launch a save
            network.Save(SuccessHandler, ErrorHandler);
        }

        /// <summary>
        /// Handle a little the case were the server would have a problem
        /// </summary>
        private void ErrorHandler(string error, string text)
        {
            // Deactivate the loading screen
            loadingScreen.SetActive(false);

            // Log error
            Debug.LogError("Error from the server :");
            Debug.LogError(error);
            Debug.LogError(text);
        }

        private void SuccessHandler(string res)
        {
            // Deactivate the loading screen
            loadingScreen.SetActive(false);
        }

    }
}
