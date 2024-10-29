using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    public List<GameObject> enemiesInShootingRange;
    private float lastShotTime;
    private TowerData towerData;

    private void Start() {
        enemiesInShootingRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponent<TowerData>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Enemy")) {
            enemiesInShootingRange.Add(other.gameObject);
            EnemyDestructionDelegate _del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            _del.enemyDelegate += EnemyIsDestroyed;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Enemy")) {
            enemiesInShootingRange.Remove(other.gameObject);
            EnemyDestructionDelegate _del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            _del.enemyDelegate -= EnemyIsDestroyed;
        }
    }

    private void Update() {
        ShootInEnemies();
    }

    private void EnemyIsDestroyed(GameObject enemy) {
        enemiesInShootingRange.Remove(enemy);
    }

    private void Shoot(Collider2D target) {
        GameObject bulletPrefab = towerData.CurrentLevel.bullet;

        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletLogic bulletLogic = newBullet.GetComponent<BulletLogic>();
        bulletLogic.target = target.gameObject;
        bulletLogic.startPosition = startPosition;
        bulletLogic.targetPosition = targetPosition;
    }

    private void ShootInEnemies() {
        GameObject target = null;
        float minimalEnemyDistance = float.MaxValue;
        foreach(GameObject enemy in enemiesInShootingRange) {
            float distanceToGoal = enemy.GetComponent<EnemyMovement>().DistanceToGoal();
            if(distanceToGoal < minimalEnemyDistance) {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }
        if(target != null) {
            if(Time.time - lastShotTime > towerData.CurrentLevel.fireRate) {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1));
        }
    }
}
