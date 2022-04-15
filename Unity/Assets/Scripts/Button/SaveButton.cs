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
        [SerializeField] private ShowSimpleInformation simpleInformationWindow;

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
            // Log error
            Debug.LogError("Error from Network :" + error);
            Debug.Log("Server information :" + text);

            simpleInformationWindow.Show(error, 20f);

            // Close the loading screen
            loadingScreen.SetActive(false);
        }

        private void SuccessHandler(string res)
        {
            // Deactivate the loading screen
            loadingScreen.SetActive(false);

            simpleInformationWindow.Show("Code" + res, 36f);
        }

    }
}
