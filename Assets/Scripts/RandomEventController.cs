using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RoomEventData
{
    public GameObject room;
    public bool eventActive = false;
}

public enum EventType
{
    Disappear,
    Replace,
    Appear
}

[System.Serializable]
public class EventSettings
{
    public GameObject room; // Associated room for this event
    public EventType eventType;
    public GameObject[] objectsToAffect;
    public GameObject replacementObject; // Only used for EventType.Replace
    public GameObject objectToAppear; // Only used for EventType.Appear
}

public class RandomEventController : MonoBehaviour
{
    public float minEventInterval = 30f; // Minimum time interval between events
    public float maxEventInterval = 60f; // Maximum time interval between events
    public EventSettings[] eventSettings; // Array of event settings

    private float nextEventTime; // Time when the next event will occur
    private Dictionary<GameObject, EventSettings> activeEvents = new Dictionary<GameObject, EventSettings>(); // Map of active events per room

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
        // Choose a random event setting
        EventSettings randomEventSetting = eventSettings[Random.Range(0, eventSettings.Length)];

        // Check if the event's room already has an active event
        if (activeEvents.ContainsKey(randomEventSetting.room))
        {
            // An active event is already happening in this room, skip triggering a new event
            return;
        }

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
                        Instantiate(randomEventSetting.replacementObject, obj.transform.position, obj.transform.rotation, randomEventSetting.room.transform);
                        Destroy(obj);
                    }
                }
                break;
            case EventType.Appear:
                if (randomEventSetting.objectToAppear != null)
                {
                    // Activate the object
                    randomEventSetting.objectToAppear.SetActive(true);
                }
                break;
        }

        // Mark the room as having an active event
        activeEvents[randomEventSetting.room] = randomEventSetting;
    }
}
