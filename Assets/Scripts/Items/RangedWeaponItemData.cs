using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon Item Data", menuName = "Item/Ranged Weapon Item Data")]
public class RangedWeaponItemData : ItemData
{
    [Header("Ranged Weapon Item Data")]
    public float FireRate;
    public GameObject ProjectilePrefab;
    public ItemData ProjectileItemData;

    public void Fire(Vector3 spawnposition, Quaternion spawnrotation, Character.Team team )
    {
        GameObject proj = Instantiate(ProjectilePrefab, spawnposition, spawnrotation);
        proj.GetComponent<Projectile>().SetTeam(team);
    }
}
