using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Dynamic;

public class Player : MonoBehaviour
{
    public Text coinText;
    public int currentCoin = 0;
    public int maxHealth = 3;
    public Text health;
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpheight = 5f;
    public bool isGround = true;
    public bool isLaser = false;
    public bool isGrenade = true;
    public GameObject bullet;
    public GameObject GrenadePreFab;

    private LineRenderer lr;
    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;

    public Transform attackPoint;
    public float attackRadius;
    public LayerMask attackLayer;

    [Header("Player Shoot")]
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask targetLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        lr.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0) {
            Die();

        }

        health.text = maxHealth.ToString();
        coinText.text = currentCoin.ToString();

        movement = Input.GetAxis("Horizontal");

        if (movement < 0f && facingRight) {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;

        }
        else if (movement > 0f && facingRight == false) {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;

        }

        if (Input.GetKey(KeyCode.Space) && isGround) {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);
        }

        if (Mathf.Abs(movement) > .1f) {
            animator.SetFloat("Run", 1f);
        }
        else if (movement < .1f) {
            animator.SetFloat("Run", 0f);
        }

        if (Input.GetMouseButtonDown(1)) {
            if (isLaser) {
                StartCoroutine(Laser());
                return;
            }
            if (isGrenade) {
                Grenade();
                return;
	        }
            Firing();

        }

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Grenade() {
        Instantiate(GrenadePreFab, ShootPoint.position, ShootPoint.rotation);
    }

    void Firing()
    {
        Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
    }


        IEnumerator Laser() {
            lr.enabled = true;
            lr.SetPosition(0, ShootPoint.position);
            RaycastHit2D hit = Physics2D.Raycast(ShootPoint.position, ShootPoint.right, distance, targetLayer);
            if (hit)
            {
                lr.SetPosition(1, hit.point);
                if (hit.transform.gameObject.GetComponent<Patrol_Enemy>() != null)
                {
                    hit.transform.gameObject.GetComponent<Patrol_Enemy>().TakeDamage(2);
                }
            }
            else
            {
                lr.SetPosition(1, ShootPoint.position + ShootPoint.right * distance);
            }

            yield return null;
            lr.enabled = false;
        }

        void Jump() {
            rb.AddForce(new Vector2(0f, jumpheight), ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground") {
                isGround = true;
                animator.SetBool("Jump", false);
            }
        }

        public void Attack() {
            Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
            if (collInfo) {
                if (collInfo.gameObject.GetComponent<Patrol_Enemy>() != null) {
                    collInfo.gameObject.GetComponent<Patrol_Enemy>().TakeDamage(1);

                }
            }
        }

        void OnDrawGizmosSelected()
        {

            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        public void TakeDamage(int damage) {
            if (maxHealth <= 0) {
                return;
            }
            maxHealth -= damage;

        }

        void OnTriggerEnter2D(Collider2D other) {

            if (other.gameObject.tag == "Coin") {
                currentCoin++;
                other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collected");
                Destroy(other.gameObject, 1f);
            }
            if (other.gameObject.tag == "VictoryPoint") {

                FindObjectOfType<SceneManageMent>().LoadLevel();
            }



        }

        void Die() {
            FindObjectOfType<GameManager>().isGameActive = false;
            Destroy(this.gameObject);
        }

    } 
