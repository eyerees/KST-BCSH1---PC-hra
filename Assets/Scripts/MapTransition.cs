using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundary; 
    
    [SerializeField] private Direction direction; 
    [SerializeField] private float transitionDistance = 2f;

    private CinemachineConfiner2D confiner; 

    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);

            MapController_Manual.Instance.HiglightArea(mapBoundary.name);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += transitionDistance; 
                break;
            case Direction.Down:
                newPos.y -= transitionDistance; 
                break;
            case Direction.Left:
                newPos.x -= transitionDistance; 
                break;
            case Direction.Right:
                newPos.x += transitionDistance; 
                break;
        }

        player.transform.position = newPos;
    }
}