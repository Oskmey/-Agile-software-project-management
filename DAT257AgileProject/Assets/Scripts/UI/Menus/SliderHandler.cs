using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderHandler : MonoBehaviour
{
    private EventTrigger eventTrigger;

    private void Awake()
    {
        if (!TryGetComponent<EventTrigger>(out eventTrigger))
        {
            Debug.LogError("Slider Event Trigger was null");
        }

        InitializeEventTrigger();
    }

    private void InitializeEventTrigger()
    {
        // Create a new entry for the event trigger
        EventTrigger.Entry entry = new()
        {
            // Set the event type to PointerClick
            eventID = EventTriggerType.PointerClick
        };
        // Add the listener to the entry
        entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        // Add the entry to the triggers list
        eventTrigger.triggers.Add(entry);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SliderHandler.OnPointerClick()");
        AudioManager.Instance.PlaySound(SoundName.SliderInteraction);
    }
}
