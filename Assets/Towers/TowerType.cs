using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("New Tower Type"))]

public class TowerType : ScriptableObject {

    [SerializeField] GameObject towerPrefab;

    public GameObject GetTowerPrefab()
    {
        return towerPrefab;
    }
}
