using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour 
{
    public Text WeaponText;

    void Update()
    {
        UpdateElements();
    }

    public void UpdateElements()
    {
        uint ammo = GameController.instance.PlayerWeapon.Ammo;
        uint mag = GameController.instance.PlayerWeapon.Mags;

        WeaponText.text = ammo + "\\" + mag;
    }
}
