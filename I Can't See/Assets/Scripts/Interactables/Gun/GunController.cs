using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : Holdable
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject barrel;
    
    [SerializeField]
    private AudioSource gunAudioSource;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float fireDelay;

    private void Update()
    {
        if(fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
    }

    public override void DoAction(VRHand hand)
    {
        base.DoAction(hand);

        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            if(fireDelay <= 0)
            {
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        gunAudioSource.PlayOneShot(gunAudioSource.clip);

        GameObject currentBullet = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        currentBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletSpeed);
    }
}

// Connor Done
