using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsForMerge : MonoBehaviour
{
    [SerializeField] private GameObject itemForMerge;
    [SerializeField] private float dropRate;
    private float lastDropTime;

    private void Start() {
        lastDropTime = Time.time;
    }

    private void Update() {
        DropItem();
    }

    private void DropItem() {
        if(Time.time - lastDropTime > dropRate) {
            Instantiate(itemForMerge, transform.position, transform.rotation);
            lastDropTime = Time.time;
        }
    }
}
