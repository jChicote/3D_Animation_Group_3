using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;
using TMPro;

public class QuickTimeController : MonoBehaviour
{
    [Serializable]
    public struct TimeEvent {
        public string name;
        public KeyCode key;
        public PlayableDirector failureTimeLine;
        public bool completed;
    }

    public PlayableDirector mainTimeline;
    public TextMeshProUGUI keyView;
    public TimeEvent[] events;
    private TimeEvent ActiveEvent;

    private TimeEvent getEvent(string key) {
        foreach (TimeEvent t in events) {
            if (t.name.Equals(key)) {
                return t;
            }
        }
        throw new System.ArgumentException("Event with name: " + key + " does not exist.");
    }


    public void ActivateEvent(string key) {
        ActiveEvent = getEvent(key);
        StartCoroutine(performEvent());
    }

    private IEnumerator performEvent() {
        keyView.text = ActiveEvent.key.ToString();
        keyView.enabled = true;
        yield return new WaitForSeconds(5);
        keyView.enabled = false;
        if (!ActiveEvent.completed) {
            ActiveEvent.failureTimeLine.time = mainTimeline.time;
            mainTimeline.Stop();
            ActiveEvent.failureTimeLine.Play();
        }
    }

    public void DoLampFlicker() {
        GameObject[] lamps = GameObject.FindGameObjectsWithTag("Lamp");
        foreach (GameObject lamp in lamps) {
            lamp.GetComponent<LampControl>().DoFlicker();
        }
    }

    // Start is called before the first frame update
    void Start(){
        keyView.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(ActiveEvent.key)) {
            ActiveEvent.completed = true;
            keyView.enabled = false;
        }
    }
}
