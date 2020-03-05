using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourableHoldable : Holdable
{
    //Prefab for blobs of water
    [SerializeField]
    protected GameObject waterPrefab;

    //Location on pourable to create
    [SerializeField]
    protected Transform pourCreatePoint;

    //Min rotation for the pourable to pour at
    float minRotation = 25f;

    //Time between blobs being created in seconds
    const float timeBetweenBlobs = 0.1f;
    //Time since the last blob was released
    float timeSinceBlob;

    //Number of Water Drops that the object starts with
    [SerializeField]
    private int startingDropCount;
    //Number of water drops that we currently have
    protected int waterDropCount;

    protected void Start()
    {
        //Init Time since last blob
        timeSinceBlob = 0.0f;

        //Init number of starting drops
        waterDropCount = startingDropCount;
    }

    // Update is called once per frame
    protected void Update()
    {
        //Increment the time since the last blob
        timeSinceBlob += Time.deltaTime;

        //Check that we have water drops to drop
        if (waterDropCount > 0)
        {
            //Check if the object is rotated more than the 
            // min rotation for us to pour
            if (CalculatePourAngle() > minRotation)
            {
                //If it has been long enough since the last blob
                //the create a new one
                if (timeSinceBlob > timeBetweenBlobs)
                {
                    //Null Check Water Prefab
                    if (!waterPrefab) { return; }

                    GameObject waterDrop = Instantiate(waterPrefab);
                    waterDrop.transform.position = pourCreatePoint.position;
                    waterDrop.GetComponent<WaterDrop>().dropCreatorObject = this.gameObject;

                    //Reduce the number of blobs that this object
                    //has
                    waterDropCount--;

                    //Reset Timer
                    timeSinceBlob = 0.0f;
                }

            }
        }
    }

    /// <summary>
    /// Get the current angle of the pourable
    /// </summary>
    /// <returns></returns>
    private float CalculatePourAngle()
    {
        float xRot = transform.rotation.eulerAngles.x;
        float zRot = transform.rotation.eulerAngles.z;

        //Get the rotation in the Z and X Axis, ignoring the Y
        Quaternion RotationZX = Quaternion.Euler(new Vector3(xRot, 0, zRot));

        //Get the difference between this rotation and the default up rotation
        float rotation = Quaternion.Angle(RotationZX, Quaternion.identity);

        return rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check that we are colliding with a water drop
        WaterDrop waterDrop = collision.gameObject.GetComponent<WaterDrop>();
        if (waterDrop)
        {
            //Check that we are not the creator of this object
            if (waterDrop.dropCreatorObject != this.gameObject)
            {
                //Add to water drop count
                waterDropCount++;

                //Delete Water Drop
                Destroy(waterDrop.gameObject);
            }
        }
    }

}


//Lewis Hammond