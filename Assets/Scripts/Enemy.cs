using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public float healt = 4f;
    
    public GameObject deathEffect;
    private void Start()
    {
        EnemiesAlive++;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > healt)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effectClone = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effectClone, 2f);

        EnemiesAlive--;
        if (EnemiesAlive <= 0)
        {
            Debug.Log("LEVEL WON");
        }

        Destroy(gameObject);
    }
}
