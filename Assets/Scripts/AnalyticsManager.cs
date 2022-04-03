using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    private static Queue<string> _dataQueue = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Poll());
    }

    public void Send(string data)
    {
        data = AddTimestamp(AddID(data));
        _dataQueue.Enqueue(data);
    }

    private IEnumerator Poll()
    {
        while (true)
        {
            Debug.Log("Processing analytics queue");
            int count = _dataQueue.Count;

            while (_dataQueue.Count > 0)
            {
                Post(_dataQueue.Dequeue());
            }

            Debug.Log("Dispatched " + count + " events");
            yield return new WaitForSecondsRealtime(10f);
        }
    }

    private IEnumerator Post(string data)
    {
        string url = "http://52.18.197.119/upload_data";

        using (UnityWebRequest request = UnityWebRequest.Put(url, data))
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

    private string AddID(string data)
    {
        string ID = "{'ID':" + SystemInfo.deviceUniqueIdentifier + ",";
        data.Remove(0, 1); // Remove the first { from our data
        return ID + data;
    }

    private string AddTimestamp(string data)
    {
        
        string Time = "{'time':'" + System.DateTime.Now.ToString() + "',";
        data.Remove(0, 1);
        return Time + data;
    }
}
