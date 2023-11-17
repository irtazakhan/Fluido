using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public float moveSpeed = 3f; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (collider != null && collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            targetPosition.z = transform.position.z; // Maintain the same z-coordinate
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

           // UpdateAnimator(targetPosition - transform.position);
        }
    }

    private void UpdateAnimator(Vector3 moveDirection)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
    }
}
