using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System;
using TMPro;

public class QuickTimeController : MonoBehaviour
{
    [Serializable]
    public struct TimeEvent {
        public string name;
        public KeyCode key;
        public PlayableDirector failureTimeLine;
        public Image arrow;
        public bool completed;
        public float duration;
    }

    public PlayableDirector mainTimeline;
    public TextMeshProUGUI keyView;
    public Image ring;
    public Image branchArrow;
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
        ring.enabled = true;
        ring.transform.localScale = new Vector3(2, 2, 2);
        ActiveEvent.arrow.color = Color.white;
        ActiveEvent.arrow.enabled = true;
        keyView.text = ActiveEvent.key.ToString();
        // keyView.enabled = true;
        if (ActiveEvent.name == "branch1")
        {
            branchArrow.enabled = true;
            branchArrow.color = Color.white;
        }
        yield return new WaitForSeconds(ActiveEvent.duration);
        keyView.enabled = false;
        ring.enabled = false;
        if (!ActiveEvent.completed) {
            ActiveEvent.failureTimeLine.time = mainTimeline.time;
            mainTimeline.Stop();
            ActiveEvent.failureTimeLine.Play();
            mainTimeline = ActiveEvent.failureTimeLine;
            ActiveEvent.arrow.color = Color.red;
            yield return new WaitForSeconds(1);
            branchArrow.enabled = false;
            ActiveEvent.arrow.enabled = false;
        }
    }

    private IEnumerator callSuccessArrow()
    {
        ActiveEvent.arrow.color = Color.green;
        yield return new WaitForSeconds(1);
        ActiveEvent.arrow.enabled = false;
        ring.enabled = false;

    }

    private IEnumerator callBranchArrow()
    {
        branchArrow.color = Color.green;
        yield return new WaitForSeconds(1);
        branchArrow.enabled = false;
        ring.enabled = false;

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
        ring.enabled = false;
        branchArrow.enabled = false;
        foreach(TimeEvent item in events)
        {
            item.arrow.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(ActiveEvent.key)) {
            ActiveEvent.completed = true;
            keyView.enabled = false;
            StartCoroutine(callSuccessArrow());
            branchArrow.enabled = false;
        }
        if (ActiveEvent.name == "branch1")
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ActiveEvent.completed = false;
                keyView.enabled = false;
                StartCoroutine(callBranchArrow());
                ActiveEvent.arrow.enabled = false;
            }
        }
        if (ring.enabled == true) {
            ring.transform.localScale = ring.transform.localScale - 1 / ActiveEvent.duration * new Vector3(0.02f, 0.02f, 0.02f);
        }
        
    }
}
