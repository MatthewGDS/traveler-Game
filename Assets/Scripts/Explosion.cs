using UnityEditor.PackageManager;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int damage = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Patrol_Enemy enemy = collision.GetComponent<Patrol_Enemy>();

        if (enemy != null)
        {
            
            enemy.TakeDamage(damage);

        }
       
    }



}