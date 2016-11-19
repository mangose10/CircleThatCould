using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyTAI : MonoBehaviour {

    private Rigidbody2D tRigidBody;
    private Animator tAnimator;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private bool isGroundedL;
    [SerializeField]
    private bool isGroundedR;
    [SerializeField]
    private bool isWalled;
    [SerializeField]
    private float colRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform[] groundRightCheck;
    [SerializeField]
    private Transform[] groundLeftCheck;
    [SerializeField]
    private Transform[] wallCheck;
    private bool timer = true;
    private float timed;
    private bool facingRight = true;
    [SerializeField]
    private GameObject player;
    private bool defenseFlag = false;
    Collider2D StandingCollider;
    Collider2D DefenseCollider;
    [SerializeField]
    Canvas canvas;

    // Use this for initialization
    void Start () {
        tRigidBody = GetComponent<Rigidbody2D>();
        tAnimator = GetComponent<Animator>();
        StandingCollider = GetComponent<BoxCollider2D>();
        DefenseCollider = GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        isGroundedL = CheckLeftGround();
        isGroundedR = CheckRightGround();
        isWalled = CheckWall();
        timed += Time.deltaTime;
        timer = timerF(timer, timed);
        Flip(speed);
        ChangeCollider();
        tAnimator.SetFloat("distance", Mathf.Abs(player.transform.position.x - transform.position.x));
        tAnimator.SetBool("facing", facingPlayer());
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5 && facingPlayer())
            defenseFlag = true;
        Movement(isGroundedL, isGroundedR, isWalled);
	}
   void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider == player.GetComponent<PolygonCollider2D>() || other.collider == player.GetComponent<CircleCollider2D>())
        {
            if (!facingPlayer() && !defenseFlag)
            {
                Destroy(this.gameObject);
                SceneManager.LoadScene("new");
            }
            else
            {
                SceneManager.LoadScene("test");
            }
        }
    }

    bool facingPlayer(){
        if ((player.transform.position.y - transform.position.y < 5) && (player.transform.position.y - transform.position.y > -0.5)) {
            if (((player.transform.position.x > transform.position.x) && (speed > 0)) || ((player.transform.position.x < transform.position.x) && (speed < 0)))
                return true; 
            else
                return false;
        }
        else
            return false;
            
    }
    void Movement(bool isGroundedL, bool isGroundedR, bool isWalled){
        if (!defenseFlag)
            tRigidBody.velocity = new Vector2(speed, tRigidBody.velocity.y);
        else
            tRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY;
        if ((!isGroundedR|| !isGroundedL || isWalled) && timer && !defenseFlag) {
            speed *= -1;
            timer = false;
            timed = 0;
        }
        
    }
    bool timerF(bool timer, float timed){
        if (timed >= .2) { return true; }
        else { return false; }
    }

    private bool CheckRightGround(){
        foreach (Transform point in groundRightCheck)
        {
            Collider2D[] grColliders = Physics2D.OverlapCircleAll(point.position, colRadius, whatIsGround);

            for (int i = 0; i < grColliders.Length; i++)
            {
                if (grColliders[i].gameObject != gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckLeftGround()
    {
        foreach (Transform point in groundLeftCheck)
        {
            Collider2D[] glColliders = Physics2D.OverlapCircleAll(point.position, colRadius, whatIsGround);

            for (int i = 0; i < glColliders.Length; i++)
            {
                if (glColliders[i].gameObject != gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckWall()
    {
        foreach (Transform point in wallCheck)
        {
            Collider2D[] wColliders = Physics2D.OverlapCircleAll(point.position, colRadius, whatIsGround);

            for (int i = 0; i < wColliders.Length; i++)
            {
                if (wColliders[i].gameObject != gameObject)
                {
                    Debug.Log(wColliders[i]);
                    return true;
                }
            }
        }
        return false;
    }
    private void Flip(float horizontal)
    {
        if (((horizontal > 0) && !facingRight) || ((horizontal < 0) && facingRight))
        {

            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    void ChangeCollider()
    {
        if (defenseFlag)
        {
            StandingCollider.enabled = false;
            DefenseCollider.enabled = true;
        }
        else
        {
            StandingCollider.enabled = (true);
            DefenseCollider.enabled = (false);
        }
    }
}
