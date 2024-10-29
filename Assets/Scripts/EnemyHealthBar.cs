using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    EnemyHealth enemyHealth;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
            UpdateParams();
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", enemyHealth.currentHealth / (float)enemyHealth.maxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }
}
