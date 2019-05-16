using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles;

public class Gun : Weapon
{
    public int maxReserve = 500, maxClip = 20;
    public float spread = 2, recoil = 1, range = 10;
    public Transform shotOrigin;
    public GameObject projectilePrefab;
    [SerializeField] [Range(0, 500)] private int currentReserve = 0, currentClip = 0;
    private CameraLook camLook;

    public void Awake()
    {
        camLook = FindObjectOfType<CameraLook>();
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

    public override void Attack()
    {
        base.Attack();
        currentClip--;
        if (currentClip == 0)
        {
            Reload();
        }
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 bulletOrigin = camTransform.position;
        Quaternion bulletRotation = camTransform.rotation;
        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;

        GameObject clone = Instantiate(projectilePrefab, bulletOrigin, bulletRotation);
        Projectile projectile = clone.GetComponent<Projectile>();
        projectile.damage += damage;
        projectile.Fire(lineOrigin, direction);
        Vector3 euler = Vector3.up * 2f;
        euler.x = Random.Range(-1f, 1f);
        camLook.SetTargetOffset(euler * recoil);

    }
}
