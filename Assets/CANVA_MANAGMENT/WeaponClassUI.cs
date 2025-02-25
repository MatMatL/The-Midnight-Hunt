using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public int weaponID;
    public GameObject weapon;
    public int weaponPrice;
    public bool isUnlocked;
    public bool isEquipped;

    public Image weaponStatus;
    public TextMeshProUGUI statusText;

    public Color equippedColor = Color.green;
    public Color unlockedColor = new Color(1f, 0.5f, 0f);
    public Color lockedColor = Color.red;

    public void UpdateWeaponUI()
    {
        if (isEquipped)
        {
            weaponStatus.color = equippedColor;
            statusText.text = "Equipped";
            weapon.SetActive(true);
        }
        else if (isUnlocked)
        {
            weaponStatus.color = unlockedColor;
            statusText.text = "Unlocked";
            weapon.SetActive(false);
        }
        else
        {
            weaponStatus.color = lockedColor;
            statusText.text = weaponPrice + " golds";
            weapon.SetActive(false);
        }
    }
}
