using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
/// <summary>
/// Dictionary to hold the name and trigger actions of trigger and listeners
/// </summary>
    private Dictionary<string, TriggerAction> eventDictionary;


/// <summary>
/// Singleton of the EventManager 
/// </summary>
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    /// Creates an active Event Manager in the scene
                    eventManager = new GameObject("eventManager").AddComponent<EventManager>();
                    eventManager.Init();
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    /// <summary>
    /// Starts the event dictionary
    /// </summary>
    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, TriggerAction>();
        }
    }

    /// <summary>
    /// Subscribes the class for the UnityAction 
    /// </summary>
    /// <param name="eventName">Event name that will be triggered</param>
    /// <param name="listener">Function that will be invoked in the UnityAction</param>
    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TriggerAction();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Remove the event from the class to not be called
    /// </summary>
    /// <param name="eventName">Event name to remove</param>
    /// <param name="listener">Function that will be removed in the UnityAction</param>
    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;
        if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Check if there's an event with given name in the dictionary, so it Invokes its UnityEvent
    /// </summary>
    /// <param name="eventName">Name of the event to trigger in the dictionary</param>
    /// <param name="property">Parameter that will be sent to the UnityEvent</param>
    public static void TriggerEvent(string eventName, object property)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(property);
        }
    }
}

/// <summary>
/// Class of the 
/// </summary>
public class TriggerAction : UnityEvent<object>
{
    
}