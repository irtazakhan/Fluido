using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 targetPosition;
    public float moveSpeed = 3f; // Adjust the move speed as needed

    public bool canMove = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Start dragging
                isDragging = true;

                // Convert screen point to world point
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = transform.position.z; // Maintain the same z-coordinate
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Stop dragging
                isDragging = false;
            }

            if (isDragging)
            {
                // Convert screen point to world point continuously
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = transform.position.z; // Maintain the same z-coordinate

                // Move the player towards the dragged position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Optional: Update Animator parameters for movement
                UpdateAnimator(targetPosition - transform.position);
            }
            else
            {
                UpdateAnimator(Vector3.zero);
            }
        }
        else
        {
            isDragging = false;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }

    }
    void UpdateAnimator(Vector3 direction)
    {

        // Normalize the direction vector to get a unit vector
        direction.Normalize();

        // Set Animator parameters based on the movement direction
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
}
