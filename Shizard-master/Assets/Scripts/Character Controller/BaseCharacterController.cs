using UnityEngine;
/// <summary>
/// This adds the basic functionallity for a character to be able to move
/// </summary>
public class BaseCharacterController : MonoBehaviour
{
    [SerializeField]
    protected int health;

    //Movement parameters
    [Header("Movement Parameters")]
    [SerializeField]
    protected BaseInputController inputController;
    [SerializeField]
    protected float forwardMovementSpeed;
    [SerializeField]
    private float sideMovementSpeed;
    [SerializeField]
    private float boundaryLeft;
    [SerializeField]
    private float boundaryRight;

    protected bool controllable = true;
    protected int currentHealth;

    protected virtual void Start()
    {
        currentHealth = health;
    }

    //Update character positions based on the player input
    protected virtual void Update()
    {
        if (!IsActive())
            return;

        //Move Side
        Vector3 newPosition = transform.position + new Vector3((int)inputController.Direction, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, boundaryLeft, boundaryRight);
        transform.position = Vector3.Lerp(transform.position, newPosition, sideMovementSpeed * Time.deltaTime);

        //Move Forward
        transform.position += transform.forward * forwardMovementSpeed * Time.deltaTime;
    }

    protected bool IsActive()
    {
        return controllable && LevelManager.instance.levelStarted;
    }

    protected void UpdateHealth (int value)
    {
        currentHealth += value;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            controllable = false;
        }
    }
}