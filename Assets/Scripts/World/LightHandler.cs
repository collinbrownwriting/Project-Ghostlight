using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightHandler : MonoBehaviour
{
    public bool oscillatingLight;
    public float currentIntensity;
    public float targetIntensity;
    public float minIntense;
    public float maxIntense;
    public float oscTime;
    
    void Update()
    {
        if (oscillatingLight == true) {
            if (this.gameObject.GetComponent<Light2D>().intensity >= maxIntense){
                targetIntensity = minIntense;
            } else if (this.gameObject.GetComponent<Light2D>().intensity <= minIntense){
                targetIntensity = maxIntense;
            }
            
            this.gameObject.GetComponent<Light2D>().intensity = Mathf.MoveTowards(currentIntensity, targetIntensity, oscTime * Time.deltaTime);
            currentIntensity = this.gameObject.GetComponent<Light2D>().intensity;
        }
    }
}
