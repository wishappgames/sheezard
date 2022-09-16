using UnityEngine;

public class Bow : Weapon
{
    protected override int fireRate => 1;
    private Transform[] spawnPoints;

    public Bow(PlayerCharacterController playerCharacter, int power) : base(playerCharacter, power)
    {
        GameObject weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapons/Bow");
        GameObject weaponObject = Object.Instantiate(weaponPrefab, playerCharacter.transform);
        spawnPoints = new Transform[] { weaponObject.transform.GetChild(0), weaponObject.transform.GetChild(1) };
        StartAttack();
    }

    public override void UpdatePower(int addedPower)
    {
        power += addedPower;
    }

    protected override void Attack()
    {
        foreach (Transform spawnPoint in spawnPoints)
            Utility.SpawnProjectile(spawnPoint.position);
    }
}
