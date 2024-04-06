using UnityEngine;
using System.Collections;

public enum EventType
{
    Disappear,
    Replace
}

[System.Serializable]
public class EventSettings
{
    public EventType eventType;
    public GameObject[] objectsToAffect;
    public GameObject replacementObject; // Only used for EventType.Replace
}

public class RandomEventController : MonoBehaviour
{
    public float minEventInterval = 30f; // Minimum time interval between events
    public float maxEventInterval = 60f; // Maximum time interval between events
    public GameObject[] rooms; // Array of rooms where events can occur
    public EventSettings[] eventSettings; // Array of event settings

    private float nextEventTime; // Time when the next event will occur

    void Start()
    {
        // Set initial next event time
        nextEventTime = Time.time + Random.Range(minEventInterval, maxEventInterval);
    }

    void Update()
    {
        // Check if it's time for a new event
        if (Time.time >= nextEventTime)
        {
            // Trigger a random event
            TriggerRandomEvent();

            // Set next event time
            nextEventTime = Time.time + Random.Range(minEventInterval, maxEventInterval);
        }
    }

    void TriggerRandomEvent()
    {
        // Choose a random room
        GameObject randomRoom = rooms[Random.Range(0, rooms.Length)];

        // Choose a random event setting
        EventSettings randomEventSetting = eventSettings[Random.Range(0, eventSettings.Length)];

        // Apply the selected event type to the chosen room
        switch (randomEventSetting.eventType)
        {
            case EventType.Disappear:
                foreach (GameObject obj in randomEventSetting.objectsToAffect)
                {
                    if (obj != null)
                        Destroy(obj);
                }
                break;
            case EventType.Replace:
                foreach (GameObject obj in randomEventSetting.objectsToAffect)
                {
                    if (obj != null && randomEventSetting.replacementObject != null)
                    {
                        Instantiate(randomEventSetting.replacementObject, obj.transform.position, obj.transform.rotation, randomRoom.transform);
                        Destroy(obj);
                    }
                }
                break;
        }
    }
}
