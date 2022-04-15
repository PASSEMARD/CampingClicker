using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace clicker
{
    public delegate void CallBackFromServer(string textReceived);
    public delegate void ErrorCallBackFromServer(string error, string textReceived);

    public class MainNetworkObject : MonoBehaviour
    {
        private string host = "http://localhost/CampingClicker/";


        [SerializeField] private PlayerInformation player;
        [SerializeField] private Scorer scorer;
        [SerializeField] private GroundManager groundManager;

        public void Save(CallBackFromServer callBack, ErrorCallBackFromServer errorCallBack)
        {
            // Setup the args before sending them
            Dictionary<string, string> valuesToSend = new Dictionary<string, string>()
            {
                { "score", scorer.Score.ToString() },
                { "upgradeClick", player.RessourcePerClickInfo.actualLvl.ToString() },
                { "upgradeGatherer", player.RessourcePerTimeInfo.actualLvl.ToString() },
                { "treeMap", groundManager.GetLogicalTreeHasString() }
            };

            StartCoroutine(Post(callBack, errorCallBack, "save", valuesToSend));
        }

        public void Load(CallBackFromServer callBack, ErrorCallBackFromServer errorCallBack, string valueCode)
        {
            // Setup Args
            Dictionary<string, string> valuesToSend = new Dictionary<string, string>()
            {
                {"saveCode", valueCode}
            };

            StartCoroutine(Get(callBack, errorCallBack, "load", valuesToSend));
        }

        IEnumerator Post(CallBackFromServer callBack, ErrorCallBackFromServer errorCallback, string route, Dictionary<string, string> dataToSend = null)
        {
            WWWForm form = new WWWForm();

            if(dataToSend != null)
            {
                foreach(KeyValuePair<string, string> kvp in dataToSend)
                {
                    form.AddField(kvp.Key, kvp.Value);
                }
            }

            UnityWebRequest www = UnityWebRequest.Post(host + route, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                // Send all information to the error callback 
                errorCallback.Invoke(www.error, www.downloadHandler.text);
            }
            else
            {
                // Send all information to the callback
                callBack.Invoke(www.downloadHandler.text);
            }
        }

        IEnumerator Get(CallBackFromServer callBack, ErrorCallBackFromServer errorCallback, string route, Dictionary<string, string> dataToSend = null)
        {
            WWWForm form = new WWWForm();
            string args = "";

            if(dataToSend != null)
                args = GetStringFromArguments(dataToSend);

            UnityWebRequest www = UnityWebRequest.Get(host + route + args);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                errorCallback(www.error, www.downloadHandler.text);
            }
            else
            {
                callBack(www.downloadHandler.text);
            }
        }

        private string GetStringFromArguments(Dictionary<string, string> arguments)
        {
            string res = "";

            int argsNumber = arguments.Count;
            
            if (argsNumber == 0)
                return res;

            /* Count the number of '&' to put on the string by using argsNumber
             * So argsNumber has to be lower by 1 before starting  */
            argsNumber--;

            foreach(KeyValuePair<string, string> keyValuePair in arguments)
            {
                // Add the key value pair  
                res += keyValuePair.Key + "=" + keyValuePair.Value;

                if(argsNumber > 0)
                {
                    // Add a '&' caracter if needed
                    res += "&";
                    argsNumber--;
                }
            }

            // Add at the start, the '?' caractere needed to add argument in a request
            return "?" + res;
        }
    }
}