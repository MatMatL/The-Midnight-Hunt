using System;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject Target;
    private float sinceLastHit = 5;
    public int reloadTime;
    public GameObject Bullet;
    public int bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.GetComponent<UpDownCamera>().lookDirection);

        sinceLastHit += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && sinceLastHit > reloadTime)
        {
            sinceLastHit = 0;
            GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet.transform.LookAt(Target.GetComponent<UpDownCamera>().lookDirection);
            bullet.GetComponent<Rigidbody>().linearVelocity = transform.forward * bulletSpeed;
        }
    }
}
