using UnityEngine;
//This class handles camera movement in the scene to keep track of the player
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float speed;

    //Track player movement in the scene
    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Assign player transform");
            return;
        }

        Vector3 position = player.position - offset;
        position.x = 0;
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
    }
}