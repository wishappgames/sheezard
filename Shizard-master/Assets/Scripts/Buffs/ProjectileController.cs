using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float pierceAmount = 1;

    private void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        pierceAmount--;
        if (pierceAmount == 0 || other.tag == "End")
            Destroy(gameObject);
    }

    public void SetPierceAmount (int amount)
    {
        pierceAmount = amount;
    }
}
