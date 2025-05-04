using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Patrol_Enemy enemy = collision.GetComponent<Patrol_Enemy>();
        if (enemy != null) {
            enemy.TakeDamage(damage);

	    }
	    Destroy(gameObject);
    }



}
