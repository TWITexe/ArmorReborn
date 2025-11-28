using UnityEngine;

public class KnightMoveController : MonoBehaviour
{
    [SerializeField] float speed = 4.5f;
    private Rigidbody2D body;
    private Animator anim;

    private float moveDirection = 0f;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float deltaX;
        if (anim.GetBool("endOfAttack"))
            deltaX = moveDirection * speed;            
        else
            deltaX = 0;

        Vector2 movemetn = new Vector2(deltaX, body.linearVelocity.y);
        body.linearVelocity = movemetn;

        anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) * 3, transform.localScale.y, 1);
        }      
        
    }

    private void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
    }
    public void MoveLeft()
    {
        moveDirection = -1f;
    }

    public void MoveRight()
    {
        moveDirection = 1f;
    }

    public void StopMoving()
    {
        moveDirection = 0f;
    }


}
