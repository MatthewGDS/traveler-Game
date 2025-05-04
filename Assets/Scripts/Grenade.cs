using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;

    [SerializeField] private float radius;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionTime;
    [SerializeField] private GameObject explosionPrefab;

    public float speed = 20f;
    public Rigidbody2D rb;

    private float explosionTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        explosionTimer += Time.fixedDeltaTime;

        if(explosionTimer >= explosionTime) {
            

            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius, interactLayer);

            foreach(Collider2D coll in collisions)
            { 
	            if(coll.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {

                    Vector3 dir = rb.transform.position - transform.position;
                    rb.AddForce(dir.normalized * explosionForce);
                    

                }
	        }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
	    }
        
    }

  

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
