using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "New Stage/Stage")]
public class Stage : ScriptableObject
{
    public List<GameObject> monsters = new List<GameObject>();
    public List<Item> items = new List<Item>();
    public GameObject stageObj;
    public Stage nextStage;

}
