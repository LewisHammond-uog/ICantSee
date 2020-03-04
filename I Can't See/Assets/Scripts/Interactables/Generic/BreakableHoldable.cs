using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lewis.MathUtils;

public class BreakableHoldable : Holdable
{
    [SerializeField]
    private GameObject objectWhole;
    [SerializeField]
    private GameObject objectBroken;
    [SerializeField]
    private AudioSource interactableAudioSource;

    private Vector3 velocity = new Vector3(0,0,0);

    // Update is called once per frame
    void Update()
    {
        //Get velocity of the object before it breaks
        velocity = objectWhole.GetComponent<Rigidbody>().velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if object is moving fast enough to smash
        if(MathUtils.Abs(velocity).magnitude > 1.0f)
        {
            //Get the position of breakpoint
            Transform breakPoint = objectWhole.transform;

            //Play breaking sound
            interactableAudioSource.Play();

            //Destroy the complete object
            Destroy(objectWhole);
            //Spawn a broken version of object at position of breakpoint
            Instantiate(objectBroken, breakPoint.position, Quaternion.identity);
        }
    }
}


//Rhys Wareham