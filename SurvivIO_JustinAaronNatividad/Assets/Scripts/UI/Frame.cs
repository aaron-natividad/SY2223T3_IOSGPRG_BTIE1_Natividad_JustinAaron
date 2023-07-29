using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Frame : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weapon;
    [SerializeField] private TextMeshProUGUI otherWeapon;

    public void SetFrameInfo(Gun main, Gun other)
    {
        if (main != null)
        {
            weaponIcon.sprite = main.gunIcon;
            weaponIcon.color = Color.white;
            weapon.text = main.gunName;
        }
        
        if (other != null)
        {
            otherWeapon.text = other.gunName;
        }
    }
}
