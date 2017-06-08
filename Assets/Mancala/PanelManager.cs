using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    public Image[] pips;

    Image[] tempPips = null;
    Image panel;
    Field[] fields;
    Image temp = null;
    // Use this for initialization
    void Start() {
        panel = GetComponent<Image>();
        fields = GetComponentsInChildren<Field>();

        tempPips = pips;

        foreach (Field field in fields)
        {
            int randomNum = UnityEngine.Random.Range(0, tempPips.Length - 1);
            Instantiate(tempPips[randomNum], field.transform.position, field.transform.rotation, field.transform);

            temp = tempPips[randomNum];
            tempPips[randomNum] = tempPips[tempPips.Length -1];
            tempPips[tempPips.Length - 1] = temp;

            Array.Resize(ref tempPips, tempPips.Length - 1);
        }
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
