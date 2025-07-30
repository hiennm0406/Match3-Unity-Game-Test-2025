using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSetting", menuName = "CreateData/DataSetting", order = 1)]
public class DataSetting : ScriptableObject
{
    public List<ListSkin> lstSkin = new List<ListSkin>();
    public Sprite[] SpecialTypes;
}

[System.Serializable]
public class ListSkin
{
    public Sprite[] NormalTypes;
}
