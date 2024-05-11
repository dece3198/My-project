using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Equipment, Used, Ingredient, ETC
}

public enum UseType
{
    None, HpUp, HpDown, AtkUp, AtkDown, defUp, defDown, MpUp, Return, Gold
}

public enum IngredientType
{
    None,GlassBottle, Gem_1, Gem_2, Gem_3, Gem_4, Iron
}

public enum BladeType
{
    None, Iron_SingleEdge, Iron_DoubleEdged
}

public enum HiltType
{
    None, BasicSwordHilt, LuxurySwordHilt
}


[System.Serializable]
public class State
{
    public int amount = 0;
    public int max;
    public int min;
    public string name;
    [SerializeField, TextArea(2, 5)]
    public string content;
}


[CreateAssetMenu(fileName = " New Item",menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public Sprite itemImage;
    public GameObject itemPrefab;
    public ItemType itemType;
    public UseType useType;
    public IngredientType ingredientType;
    public BladeType bladeType;
    public HiltType hiltType;
    public Item[] upGrade;
    public int percentage;
    public State state;
}
