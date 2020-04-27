using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoint : MonoBehaviour
{
    //Distance that the front of the wave is away from the origin
    public float ScanDistance { get; private set; } = 0.0f;

    //Current width of the scan
    public float ScanWidth { get; set; } = 20f;

    //Speed that the scan should progress at per frame
    private const float scanSpd = 2.5f;

    //Max distance that the back of the wave should be 
    //before destorying the wave
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
        ScanDistance += Time.deltaTime * scanSpd;

        //Destroy if over max scan distance
        if((ScanDistance - ScanWidth) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}

//Lewis Hammond