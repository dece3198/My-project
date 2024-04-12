using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Talks
{
    public string talkerName;
    [SerializeField, TextArea(2, 5)]
    public string conversation;
    public string price;
    public Sprite image;
    public Item item;
}


[CreateAssetMenu(fileName = " New Talk", menuName = "New Talk/Talk")]
public class Talk : ScriptableObject
{
    public Talks[] talks;
}
