using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private float distanceToTargetPosition;
    private float startTime;
    private EnemyHealth enemyHealth;

    private void Start() {
        startTime = Time.time;
        distanceToTargetPosition = Vector3.Distance(startPosition, targetPosition);
        enemyHealth = target.gameObject.GetComponent<EnemyHealth>();
    } 

    private void Update() {
        BulletShooting();
    }

    private void BulletShooting() {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distanceToTargetPosition);
        if(gameObject.transform.position.Equals(targetPosition)) {
            if(target != null) {
                enemyHealth.TakeDamage(damage);
            }
        Destroy(gameObject);
        }
    }
}
