using UnityEngine;
/// <summary>
/// This class handle enemy's input based on player character position in the scene
/// </summary>
public class EnemyInputController : BaseInputController
{
    [HideInInspector]
    public PlayerCharacterController playerController;
    [SerializeField]
    private float directionUpdateInterval = 1.5f;
    [SerializeField]
    private float targetDistance = 25f;
    private float nextDirecionUpdate;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Add player object with Player tag in the scene");
            return;
        }
        playerController = player.GetComponent<PlayerCharacterController>();
    }

    void Update()
    {
        if (playerController == null)
        {
            Debug.LogError("Player object not assigned");
            return;
        }

        if (Vector3.Distance(transform.position, playerController.transform.position) >= targetDistance)
            return;

        if (Mathf.Abs(playerController.transform.position.x - transform.position.x) < 0.2f)
            dragDirection = DragDirection.None;
        else if (Time.time >= nextDirecionUpdate)
        {
            if (playerController.transform.position.x > transform.position.x)
                dragDirection = DragDirection.Right;
            else if (playerController.transform.position.x < transform.position.x)
                dragDirection = DragDirection.Left;
            nextDirecionUpdate = Time.time + directionUpdateInterval;
        }
    }
}