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
    private float minBreakVel = 10.0f;

    private Vector3 velocity = new Vector3(0,0,0);

    // Update is called once per frame
    void Update()
    {
        //If the audio soruce has not been set in the inspector try and grab it from the object
        if (interactableAudioSource == null)
        {
            interactableAudioSource = GetComponent<AudioSource>();
        }

        //Get velocity of the object before it breaks
        velocity = objectWhole.GetComponent<Rigidbody>().velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if object is moving fast enough to smash
        if(MathUtils.Abs(velocity).magnitude > minBreakVel)
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