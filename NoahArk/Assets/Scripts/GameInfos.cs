using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "NoahArk/GameInfo", order = 1)]
public class GameInfos : ScriptableObject
{
    public List<AnimalInfo> currentAnimals;
}
