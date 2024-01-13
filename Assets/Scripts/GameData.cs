using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData")]
public class GameData : ScriptableObject
{
    public int LoadLimit;
    public int CurrentLoad;
    public int Money;
    public int Npc01Value;
}
