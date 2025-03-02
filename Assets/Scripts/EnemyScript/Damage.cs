using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHP pHealth;
    public int damage;
    public float damageCooldown;
    private bool isDamaging = false;
    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            isDamaging = true;
            StartCoroutine(damageRepeat());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {
            isDamaging = false;
        }
    }

    private IEnumerator damageRepeat()
    {
        while(isDamaging)
        {
            pHealth.TakeDamage(damage);
            Debug.Log("Damage taken");
            
            yield return new WaitForSeconds(damageCooldown);
        }
    }
}
