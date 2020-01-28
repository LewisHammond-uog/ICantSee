using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectPoint : MonoBehaviour
{

    //Distance for the scan from its orign
    private float scanDistance = 0.0f;
    public float ScanDistance { get { return scanDistance; } }
    
    public float scanSpd = 20;

    // Start is called before the first frame update
    void Start()
    {
        EffectController.ePoints.Add(this);
    }

    private void OnDisable()
    {
        EffectController.ePoints.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        scanDistance += Time.deltaTime * scanSpd;
        gameObject.name = "P:" + scanDistance;
    }
}
