using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.TestTools.Constraints;

public class AreaFlash : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public bool isRed = true;
    private SpriteRenderer[] renderers;
    private Renderer[] areaRenderers;
    public GameObject areas;
    private float startTime;
    // Start is called before the first frame update 
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        areaRenderers = areas.GetComponentsInChildren<Renderer>();
        startTime = Time.time;
        //get material of all children of areas
        // areas = areas.Get
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

            foreach (Renderer renderer in areaRenderers)
            {
                Material material = renderer.material;
                float brightness = 0.6f * value;
                // material.SetFloat("_Brightness", brightness);

                Color color = material.color;
                color.a = brightness;   
                
                // renderer.material.color.a = brightness;
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

            foreach (Renderer renderer in areaRenderers)
            {
                Material material = renderer.material;
                float brightness = -0.6f * value;
                // material.SetFloat("_Brightness", brightness);
                // material.color.a = brightness;
                Color color = material.color;
                color.a = brightness;   
            }
        }
    }
}
