using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// This class handles the player character movement and scene interactions
/// </summary>
public class PlayerCharacterController : BaseCharacterController
{
    [SerializeField]
    private Slider healthBar;

    [Header("Attack Parameters")]
    [SerializeField]
    private ProjectileController projectilePrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float attackSpeed;

    private float nextAttack = 0;
    private Dictionary<string, Action<GameObject>> collisionActions;
    private Dictionary<WeaponType, Weapon> weapons;

    protected override void Start()
    {
        base.Start();
        collisionActions = new Dictionary<string, Action<GameObject>>()
        {
            { "End", OnReachEnd },
            { "Enemy", OnEnemyHit },
            { "Buff", OnBuffCollected }
        };
        weapons = new Dictionary<WeaponType, Weapon>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsActive() && Time.time >= nextAttack)
        {
            Utility.SpawnProjectile(spawnPoint.position);
            nextAttack = Time.time + attackSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health <= 0)
            return;

        if (collisionActions.TryGetValue(other.tag, out var action))
            action?.Invoke(other.gameObject);
    }

    private void OnReachEnd(GameObject hitObject)
    {
        StopAllCoroutines();
        controllable = false;
        LevelManager.instance.OnLevelPassed(true);
    }

    private void OnEnemyHit(GameObject hitObject)
    {
        UpdateHealth(-1);
        if (currentHealth <= 0)
        {
            LevelManager.instance.OnLevelPassed(false);
            StopAllCoroutines();
        }
        healthBar.value = currentHealth / (float)health;
        Destroy(hitObject);
    }

    private void OnBuffCollected(GameObject hitObject)
    {
        if (hitObject.TryGetComponent<WeaponBuff>(out var weaponBuff))
        {
            if (weapons.ContainsKey(weaponBuff.WeaponType))
                weapons[weaponBuff.WeaponType].UpdatePower(weaponBuff.Power);
            else
                weapons.Add(weaponBuff.WeaponType, Utility.CreateWeapon(this, weaponBuff.WeaponType, weaponBuff.Power));
        }
        Destroy(hitObject);
    }
}