using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    PlayerHealth playerHealth;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
            UpdateParams();
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", playerHealth.currentHealth / (float)playerHealth.maxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }
}
