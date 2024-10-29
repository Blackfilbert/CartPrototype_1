using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    private GameObject towerPrefab;
    private GameObject tower;
    private TowerStash towerStash;

    private void Start() {
        towerStash = GameObject.Find("TowerStash").GetComponent<TowerStash>();
    }

    private void Update() {
        CheckAndChangeTowerPrefabByMerge();
    }

    private void OnMouseUp() {
        if(CanPlaceTower()) {
            tower = (GameObject)
                Instantiate(towerPrefab, transform.position, Quaternion.identity);
            towerStash.towerPrefab = null;
        }
        else if(CanUpgradeTower()) {
            tower.GetComponent<TowerData>().IncreaseLevel();
        }
    }

    private bool CanPlaceTower() {
        int cost = towerPrefab.GetComponent<TowerData>().levels[0].cost;
        return tower == null;
    }

    private bool CanUpgradeTower() {
        if(tower != null) {
            TowerData towerData = tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.GetNextLevel();
            if(nextLevel != null) {
                return true;
            }
        }
        return false;
    }

    private void CheckAndChangeTowerPrefabByMerge() {
        towerPrefab = towerStash.towerPrefab;
    }
}
