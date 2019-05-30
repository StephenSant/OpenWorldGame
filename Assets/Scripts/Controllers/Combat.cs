using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

[RequireComponent(typeof(Player), typeof(CameraLook))]
public class Combat : MonoBehaviour, IHealth
{
    [Range(0,100)]public int health = 100;
    public Weapon currentWeapon;
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;

    //private Player player;
    private CameraLook cameraLook;

    void Awake()
    {
        //player = GetComponent<Player>();
        cameraLook = GetComponent<CameraLook>();
    }

    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>().ToList();
        SelectWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisableAllWeapons();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        if (currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if (fire1)
            {
                if (currentWeapon.canShoot)
                {
                    currentWeapon.Attack();
                }
            }
        }
    }
    void DisableAllWeapons()
    {
        foreach (var item in weapons)
        {
            item.canShoot = false;
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
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            print("YOU'RE DEAD!");
        }
    }
    public void Heal(int heal)
    {
        health += heal;
    }
}
