using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using UnityEngine;

public class AllAreaFlash : MonoBehaviour
{
    public GameObject red_area;
    public GameObject blue_area;
    public float pulseSpeed = 2f;
    private SpriteRenderer[] red_renderers;
    private SpriteRenderer[] blue_renderers;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        red_renderers = red_area.GetComponentsInChildren<SpriteRenderer>();
        blue_renderers = blue_area.GetComponentsInChildren<SpriteRenderer>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float value = Mathf.Sin(Time.time * pulseSpeed);
        if(value > 0)
        {
            foreach (SpriteRenderer renderer in red_renderers)
            {
                Color color = renderer.color;
                float brightness = 0.6f * (Mathf.Sin((Time.time) * pulseSpeed) * 0.5f + 0.5f);
                color.a = brightness;
                renderer.color = color;
            }
        }
        else
        {
            value = 0;
        }
        
        foreach (SpriteRenderer renderer in blue_renderers)
        {
            Color color = renderer.color;
            float brightness = 0.6f * (Mathf.Sin((Time.time) * pulseSpeed + Mathf.PI/2f) * 0.5f + 0.5f);
            color.a = brightness;
            renderer.color = color;
        }
    }
}
