using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Rotate : MonoBehaviour
{
    public int rotateSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotate();
    }

    void UpdateRotate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}
