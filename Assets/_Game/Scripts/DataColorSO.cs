using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataColor", menuName = "ScriptableObjects/DataColor", order = 1)]
public class DataColorSO : ScriptableObject
{
    public List<Color> colors;
    public Color GetColorRandom(){
        return colors[Random.Range(0,colors.Count)];
    }
}
