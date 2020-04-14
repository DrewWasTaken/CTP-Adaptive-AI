using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;

    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

    }

    void Shoot ()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~LayerMask.NameToLayer("Enemy")))
        {
            //Debug.Log(hit.transform.name);

           Enemy target = hit.transform.GetComponent<Enemy>();
           if (target != null)
           {
               target.OnHit(damage);
           }

           if(hit.rigidbody != null)
           {
               hit.rigidbody.AddForce(-hit.normal * impactForce);
           }

          GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
          Destroy(impactGO, 2f);
        }
    }
}