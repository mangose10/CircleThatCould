using UnityEngine;
//using System.Collections;

public class playerMovementR : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    [SerializeField]
    float speed = 4;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private Transform[] wallPoints;
    [SerializeField]
    private float groundRadius;
    private bool facingRight = true;
    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded = true;
    private bool jump;
    [SerializeField]
    private float jumpSpeed;

    Collider2D StandingCollider;
    Collider2D RollingCollider;

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent <Rigidbody2D> ();
        myAnimator = GetComponent<Animator>();
        StandingCollider = GetComponent<PolygonCollider2D>();
        RollingCollider = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        isGrounded = IsGrounded();
        Movement(horizontal, vertical);
        Flip(horizontal);
        jump = false;
        ChangeCollider(horizontal);
        
    }

    void ChangeCollider(float horizontal)
    {
        if (Mathf.Abs(horizontal) > 0.2)
        {
            StandingCollider.enabled = false;
            RollingCollider.enabled = true;
        }
        else
        {
            StandingCollider.enabled = (true);
            RollingCollider.enabled = (false);
        }
    }

    void Movement(float horizontal, float vertical) {
        if (!isWalled() || isGrounded)
            myRigidBody.velocity = new Vector2(horizontal * speed, myRigidBody.velocity.y);
        else
            myRigidBody.AddForce(new Vector2(0, -2));
        
        if (isGrounded && (vertical > 0.2)){
            jump = true;
        }
        if (isGrounded && jump)
        {
            myRigidBody.AddForce(new Vector2(0, jumpSpeed));
            isGrounded = false;
        }
        myAnimator.SetFloat("move", Mathf.Abs(horizontal));
    }
    private void Flip(float horizontal){
        if (((horizontal > 0) && !facingRight) || ((horizontal < 0) && facingRight)){

            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    private bool IsGrounded() {
        if (myRigidBody.velocity.y <= 0){
            foreach(Transform point in groundPoints){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++){
                    if (colliders[i].gameObject != gameObject){
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool isWalled()
    {
        if (myRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in wallPoints)
            {
                Collider2D[] wcolliders = Physics2D.OverlapCircleAll(point.position, groundRadius*4, whatIsGround);

                for (int i = 0; i < wcolliders.Length; i++)
                {
                    if (wcolliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
