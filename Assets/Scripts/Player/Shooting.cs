using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Player),typeof(CameraLook))]
public class Shooting : MonoBehaviour
{
    public Weapon currentWeapon;
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;

    private Player player;
    private CameraLook cameraLook;

    void Awake()
    {
        player = GetComponent<Player>();
        cameraLook = GetComponent<CameraLook>();
    }

    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>().ToList();
        SelectWeapon(0);
    }

    void Update()
    {
        if (currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if (fire1)
            {
                if (currentWeapon.canShoot)
                {
                    currentWeapon.Shoot();
                    Vector3 euler = Vector3.up * 2f;
                    euler.x = Random.Range(-1f, 1f);
                    cameraLook.SetTargetOffset(euler * currentWeapon.recoil);
                }
            }
        }
    }
    void DisableAllWeapons()
    {
        foreach (var item in weapons)
        {
            item.gameObject.SetActive(false);
        }
    }

    void SelectWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            DisableAllWeapons();
            currentWeapon = weapons[index];
            currentWeapon.gameObject.SetActive(true);
            currentWeaponIndex = index;
        }
    }
}
