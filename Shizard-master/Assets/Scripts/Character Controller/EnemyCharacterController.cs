using UnityEngine;
/// <summary>
/// This class handles the enemy character movement and scene interactions
/// </summary>
public class EnemyCharacterController : BaseCharacterController
{
    //Detect character interaction with scene objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            controllable = false;
        else if (other.tag == "Projectile")
        {
            UpdateHealth(-1);
            if (currentHealth <= 0)
                Destroy(gameObject);
        }
    }
}