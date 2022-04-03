using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

[SerializeField, HideInInspector]
public enum EventID
{
    PRE_GAME,
    TOWER_BUY,
    TOWER_SELL,
    TOWER_UPGRADE,
    POST_ROUND
}

[SerializeField, HideInInspector]
public enum TowerType
{
    DART,
    TREBUCHET
}

[SerializeField, HideInInspector]
public enum UpgradeType
{
    RANGE,
    FIRE_RATE,
    NULL
}

[SerializeField, HideInInspector]
public struct GameStart
{
    public const EventID id = EventID.PRE_GAME;
    public int startingWaveSize;
    public float waveIncreaseFactor;
    public Dictionary<TowerType, int> towerBaseCost;
    public Dictionary<TowerType, Dictionary<UpgradeType, int>> upgradeCost;
    public Dictionary<TowerType, Dictionary<UpgradeType, float>> upgradePriceMultiplier;
}

[SerializeField, HideInInspector]
public struct TowerEvent
{
    public EventID id;
    public TowerType type;
    public UpgradeType upgradeType;
    public int UID;
    public int value;
    public Vector2 position;
}

[SerializeField, HideInInspector]
public struct RoundEnd
{
    public const EventID id = EventID.POST_ROUND;
    public Dictionary<int, int> killCountPerTower;
    public int livesLost;
    public int moneyEarned;
}

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
        Debug.Log("Enqueueing new event");
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
