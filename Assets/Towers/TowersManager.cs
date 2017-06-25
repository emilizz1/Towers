using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class TowersManager : MonoBehaviour {

    [SerializeField] TowerType[] towerTypes;

    public TowerType currentlySelected;

    private GameObject currentlySelectedPrefab = null;
    private CameraRaycaster cameraRaycaster;
    private const int towerBuildLayerNumber = 10;
    private Tower[] towers;
    private  Vector3 towerOffset = new Vector3(0f,1f,0f);
     
	// Use this for initialization
	void Start () {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += BuildTower;
        
	}
	
	// Update is called once per frame
	void Update () {
		if(currentlySelectedPrefab != currentlySelected.GetTowerPrefab())
        {
            currentlySelectedPrefab = currentlySelected.GetTowerPrefab();
        }
	}

    void BuildTower(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit == towerBuildLayerNumber && IsThereATower(raycastHit.transform.position))
        {
            Instantiate(currentlySelectedPrefab, raycastHit.transform.position + towerOffset, Quaternion.identity, gameObject.transform);
        }
    }

    private bool IsThereATower(Vector3 position)
    {
        towers = GetComponentsInChildren<Tower>();
        foreach(Tower tower in towers)
        {
            if (tower.transform.position == position + towerOffset)
            {
                return false;
            }
        }
        return true;
    }
}
