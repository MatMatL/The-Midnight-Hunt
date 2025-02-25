using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public WeaponUI[] weapons;
    private int equippedWeaponID = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        weapons[0].isUnlocked = true;
        weapons[0].isEquipped = true;
        UpdateAllWeapons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) HandleWeaponAction(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) HandleWeaponAction(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) HandleWeaponAction(2);
    }

    private void HandleWeaponAction(int id)
    {
        WeaponUI weapon = weapons[id];

        if (!weapon.isUnlocked)
        {
            BuyWeapon(id);
        }
        else
        {
            EquipWeapon(id);
        }
    }

    private void BuyWeapon(int id)
    {
        WeaponUI weapon = weapons[id];

        if (GameManager.Instance.playerGold >= weapon.weaponPrice)
        {
            GameManager.Instance.playerGold -= weapon.weaponPrice;
            weapon.isUnlocked = true;
            EquipWeapon(id);
        }
        else
        {
            Debug.Log("not enouth gold");
        }
    }

    private void EquipWeapon(int id)
    {
        foreach (WeaponUI w in weapons)
            w.isEquipped = false;

        weapons[id].isEquipped = true;
        equippedWeaponID = id;
        UpdateAllWeapons();
    }

    private void UpdateAllWeapons()
    {
        foreach (WeaponUI weapon in weapons)
        {
            weapon.UpdateWeaponUI();
        }
    }

}
