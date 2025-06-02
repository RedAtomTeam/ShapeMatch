using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelsConfig")]
public class LevelsConfig : ScriptableObject
{
    public List<Level> levels = new List<Level>();
}


[Serializable] 
public class Level
{
    public int id;
    public string sceneName;
    public int status;
}