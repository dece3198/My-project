using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    King,Assassinm,Nobility
}

[System.Serializable]
public class Talks
{
    public string talkerName;
    [SerializeField, TextArea(2, 5)]
    public string conversation;
    public string price;
    public Sprite image;
    public Item item;
    public int talkNumber;
}


[CreateAssetMenu(fileName = " New Talk", menuName = "New Talk/Talk")]
public class Talk : ScriptableObject
{
    public Talks[] talks;
    public CharacterState characterState;
}
