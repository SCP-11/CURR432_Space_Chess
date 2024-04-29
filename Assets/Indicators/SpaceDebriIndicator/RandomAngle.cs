using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAngle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(Random.Range(0, 30), 0, Random.Range(0, 30));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
