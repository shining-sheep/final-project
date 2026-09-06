using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="RPG Setup/Sikll Data",fileName ="skill data -")]

public class SkillDataSO : ScriptableObject
{
    public int cost;
    [Header("¼¼ÄÜÃèÊö")]
    public string displayName;
    public string description;
    public Sprite icon;
}
