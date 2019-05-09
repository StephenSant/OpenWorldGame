using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10,
               maxReserve = 500, 
               maxClip = 20;
    public float spread = 2,
                 recoil = 1,
                 range = 10,
                 shootRate = .2f;
    public Transform shotOrigin;
    public GameObject bulletPrefab;
    [HideInInspector]
    public bool canShoot = false;

    private float shootTimer = 0;
    [SerializeField][Range(0,500)]
    private int currentReserve = 0, currentClip = 0;

    void Start()
    {
        currentReserve = maxReserve;
        currentClip = maxClip;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer>=shootRate)
        {
            canShoot = true;
        }
    }

    public void Reload()
    {
        if (currentReserve > 0)
        {
            if (currentClip >= 0)
            {
                currentReserve += currentClip;
                currentClip = 0;

                if (currentReserve >= maxClip)
                {
                    currentReserve -= maxClip - currentClip;

                    currentClip = maxClip;
                }
                else if (currentReserve < maxClip)
                {
                    int tempMag = currentReserve;
                    currentClip = tempMag;
                    currentReserve -= tempMag;
                }
            }
        }
    }

    public void Shoot()
    {
        currentClip--;
        if (currentClip == 0)
        {
            Reload();
        }
        shootTimer = 0;
        canShoot = false;
        Camera attachedCamera = Camera.main; 
        Transform camTransform = attachedCamera.transform;
        Vector3 bulletOrigin = camTransform.position;
        Quaternion bulletRotation = camTransform.rotation;
        Vector3 lineOrigin = shotOrigin.position; 
        Vector3 direction = camTransform.forward;

        GameObject clone = Instantiate(bulletPrefab, bulletOrigin, bulletRotation);
        Bullet bullet = clone.GetComponent<Bullet>();
        bullet.Fire(lineOrigin, direction);
    }
}
