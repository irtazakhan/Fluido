using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 targetPosition;
    public float moveSpeed = 3f; // Adjust the move speed as needed

    public bool canMove = true;

    void Update()
    {
        if(canMove)
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
        }
       
    }

    private void UpdateAnimator(Vector3 moveDirection)
    {
        // Optional: Update Animator parameters for movement
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
    }
}
