using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEquipItem : EquipItem
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private AudioClip shootSFX;
    private float lastAttackTime;

    public override void OnUse()
    {
        RangedWeaponItemData i = item as RangedWeaponItemData;

        if (Time.time - lastAttackTime < i.FireRate)
        {
            return;
        }
        
        // Return if we don't have a Projectile in our Inventory
        if (Inventory.Instance.HasItem(i.ProjectileItemData) == false)
        {
            return;
        }

        lastAttackTime = Time.time;

        // spawn a projectile
        i.Fire(muzzle.position, muzzle.rotation, Character.Team.Player);
        // remove projectile from inventory
        Inventory.Instance.RemoveItem(i.ProjectileItemData);
        // Play the sound effect
        AudioManager.Instance.PlayPlayerSound(shootSFX);
    }
}
