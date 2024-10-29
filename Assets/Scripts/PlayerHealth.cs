using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;
    private EnemyMovement enemyMovement;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        Die();
    }

    private void Die() {
        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            currentHealth -= enemyMovement.damage;
            Destroy(other.gameObject);
        }
    }
}
