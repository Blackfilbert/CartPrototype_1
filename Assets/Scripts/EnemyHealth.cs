using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;
    // [SerializeField] private GameObject itemForMerge;
    // [SerializeField] private float dropRate;
    // private float lastDropTime;

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

    public void TakeDamage(float damage) {
        currentHealth -= damage;
    }

    // private void DropItem() {
    //     if(Time.time - lastDropTime > dropRate) {
    //         Instantiate(itemForMerge, transform.position, transform.rotation);
    //         lastDropTime = Time.time;
    //     }
    // }
}
