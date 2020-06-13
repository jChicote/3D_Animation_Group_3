using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampControl : MonoBehaviour
{
    // Start is called before the first frame update
    Light[] lights;
    void Start() {
        lights = GetComponentsInChildren<Light>();
    }

    public void DoFlicker() {
        StartCoroutine(DoFlickerCoroutine());
    }

    IEnumerator DoFlickerCoroutine() {
        SwitchOff();
        yield return new WaitForSeconds(0.2f);
        SwitchOn();
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++) {
            SwitchOff();
            yield return new WaitForSeconds(0.1f);
            SwitchOn();
        }
        SwitchOff();
        yield return new WaitForSeconds(2);
        SwitchOn();
    }

    public void SwitchOff() {
        foreach (Light l in lights) {
            l.enabled = false;
        }
    }

    public void SwitchOn() {
        foreach (Light l in lights) {
            l.enabled = true;
        }
    }
}
