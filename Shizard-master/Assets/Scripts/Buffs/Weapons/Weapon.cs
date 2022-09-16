using System.Collections;
using UnityEngine;

public abstract class Weapon
{
    protected PlayerCharacterController playerCharacter;
    protected int power;
    protected abstract int fireRate { get; }

    public Weapon(PlayerCharacterController playerCharacter, int power)
    {
        this.playerCharacter = playerCharacter;
    }

    public virtual void UpdatePower(int addedPower)
    {
        power += addedPower;
    }

    protected void StartAttack ()
    {
        playerCharacter.StartCoroutine(WeaponAttack());
    }

    protected abstract void Attack();

    private IEnumerator WeaponAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(fireRate);
        }
    }
}
