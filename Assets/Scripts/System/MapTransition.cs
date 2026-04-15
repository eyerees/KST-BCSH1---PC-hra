using UnityEngine;
using Unity.Cinemachine;
using System.Threading.Tasks;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundary;
    [SerializeField] private Direction direction;
    [SerializeField] private Transform teleportTargetPosition;
    [SerializeField] private float transitionDistance = 2f;

    private CinemachineConfiner2D confiner;
    private static bool isTransitioning = false;

    public enum Direction { Up, Down, Left, Right, Teleport }

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTransitioning || !collision.CompareTag("Player")) return;

        isTransitioning = true;
        await FadeTransition(collision.gameObject);
        isTransitioning = false;
    }

    private async Task FadeTransition(GameObject player)
    {
        await ScreenFader.Instance.FadeOut();

        if (confiner != null)
        {
            confiner.BoundingShape2D = mapBoundary;
            confiner.InvalidateBoundingShapeCache(); 
        }

        UpdatePlayerPosition(player);

        await Task.Yield();

        MapController_Manual.Instance.HiglightArea(mapBoundary.name);

        await ScreenFader.Instance.FadeIn();
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        if (direction == Direction.Teleport)
        {
            if (teleportTargetPosition != null)
            {
                player.transform.position = teleportTargetPosition.position;
            }
            return;
        }

        Vector3 newPos = player.transform.position;
        switch (direction)
        {
            case Direction.Up:    newPos.y += transitionDistance; break;
            case Direction.Down:  newPos.y -= transitionDistance; break;
            case Direction.Left:  newPos.x -= transitionDistance; break;
            case Direction.Right: newPos.x += transitionDistance; break;
        }
        player.transform.position = newPos;
    }
}