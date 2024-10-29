using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeItems : MonoBehaviour
{
    [SerializeField] private GameObject tower1;
    private TowerStash towerStash;

    private void Start() {
        towerStash = GameObject.Find("TowerStash").GetComponent<TowerStash>();
    }


    private void OnMouseDrag() {
        Plane dragPlane = new Plane(Camera.main.transform.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0f;
        if(dragPlane.Raycast(ray, out enter)) {
            Vector3 fingerPosition = ray.GetPoint(enter);
            transform.position = fingerPosition;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Mergable") {
            towerStash.towerPrefab = tower1;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
