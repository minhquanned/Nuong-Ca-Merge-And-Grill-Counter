using UnityEngine;

public class PetMoverment : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // tốc độ di chuyển
    private Vector2 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void MoveTo(Vector2 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Kiểm tra nếu đã đến đích
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
