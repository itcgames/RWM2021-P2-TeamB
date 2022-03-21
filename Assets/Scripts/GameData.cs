using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public struct Data
{
    public string id;
    public int completion_time;
    public int level;
    public int moneySpent;
    public int towersBought;
}

public class GameData : MonoBehaviour
{
    public IEnumerator PostMethod(string jsonData)
    {
        Debug.Log("Started");
        string url = "http://52.18.197.119/upload_data";
        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
                Debug.Log("Data successfully sent to the server");
            else
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
        }
    }

}
