using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    [SerializeField] Image[] pips;

    private Field[] fields;

    // Use this for initialization
    void Awake()
    {
        fields = GetComponentsInChildren<Field>();

        PlacingPips(30f);
        PlacingPips(-30f);

        gameObject.SetActive(false);
    }

    public void FieldSelected(Field selected, int pipsInIt)
    {
        int tempCount = 0, selectedNumber =0;
        foreach(Field field in fields)
        {
            field.fieldIsMovable = false;
            field.CheckForColorsAtStart();
            if(field == selected)
            {
                selectedNumber = tempCount;
            }
            else
            {
                tempCount++;
            }
        }

        placableFields(pipsInIt, selectedNumber +1);
    }

    void placableFields(int pipsInIt, int selectedNumber)
    {
        for (int i = 0; i < pipsInIt; i++)
        {
            if (selectedNumber + i == fields.Length)
            {
                selectedNumber -= fields.Length;
            }
            fields[selectedNumber + i].PlacableField();
        }
    }

    private void PlacingPips(float placement)
    {
        Image[] tempPips = null;
        Image temp = null;
        tempPips = pips;

        foreach (Field field in fields)
        {
            int randomNum = UnityEngine.Random.Range(0, tempPips.Length - 1);
            Instantiate(tempPips[randomNum], field.transform.position + new Vector3(placement,0f,0f), field.transform.rotation, field.transform);

            temp = tempPips[randomNum];
            tempPips[randomNum] = tempPips[tempPips.Length - 1];
            tempPips[tempPips.Length - 1] = temp;

            Array.Resize(ref tempPips, tempPips.Length - 1);
        }
    }
}
