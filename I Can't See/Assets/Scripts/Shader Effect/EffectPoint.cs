using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoint : MonoBehaviour
{

    //Distance for the scan from its orign
    private float scanDistance = 0.0f;
    public float ScanDistance { get { return scanDistance; } }

    private float scanWidth = 20f;
    public float ScanWidth { 
        get { return scanWidth; }
        set { scanWidth = value;  }
    }

    private const float scanSpd = 3f;
    private const float maxDistance = 10f;

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
        //Increase scan distance
        scanDistance += Time.deltaTime * scanSpd;

        //Destroy if over max scan distance
        if(scanDistance > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
