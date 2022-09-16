using System.Collections.Generic;
using UnityEngine;
using System;

public static class Utility
{
    private static Dictionary<WeaponType, Func<PlayerCharacterController, int, Weapon>> createweapon;
    private static ProjectileController projectilePrefab;

    static Utility()
    {
        createweapon = new Dictionary<WeaponType, Func<PlayerCharacterController, int, Weapon>>()
        {
            { WeaponType.Bow, CreateBow }
        };
        projectilePrefab = Resources.Load<ProjectileController>("Prefabs/Projectile");
    }

    public static Weapon CreateWeapon(PlayerCharacterController playerCharacter, WeaponType weaponType, int power)
    {
        Weapon weapon = null;
        if (createweapon.TryGetValue(weaponType, out var creationAction))
            weapon = creationAction?.Invoke(playerCharacter, power);
        return weapon;
    }

    private static Weapon CreateBow(PlayerCharacterController playerCharacter, int power)
    {
        Weapon weapon = new Bow(playerCharacter, power);
        return weapon;
    }

    public static ProjectileController SpawnProjectile(Vector3 position)
    {
        return UnityEngine.Object.Instantiate(projectilePrefab, position, Quaternion.identity);
    }
}
