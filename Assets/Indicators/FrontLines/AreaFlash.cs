using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.TestTools.Constraints;

public class AreaFlash : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public bool isRed = true;
    private SpriteRenderer[] renderers;
    private float startTime;
    // Start is called before the first frame update 
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float value = Mathf.Sin(Time.time * pulseSpeed);
        if(isRed && value >= 0)
        {
            foreach (SpriteRenderer renderer in renderers)
            {
                Color color = renderer.color;
                float brightness = 0.6f * value;
                color.a = brightness;
                renderer.color = color;
            }
        }else if(!isRed && value <= 0)
        {
            foreach (SpriteRenderer renderer in renderers)
            {
                Color color = renderer.color;
                float brightness = -0.6f * value;
                color.a = brightness;
                renderer.color = color;
            }
        }
    }
}
