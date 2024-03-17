using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Vector2 startPos;
   
    // Prendre la position du départ
    private void Start()
    {
        startPos = transform.position;
    }
    
    // Si le joueur entre en collision avec le gameobject (mort) -> IL MEURT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mort"))
        {
            Die();
        }
    }

    // Fait respawn le joueur et [attente] le remet à la position de départ 
    private void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
    }
}
