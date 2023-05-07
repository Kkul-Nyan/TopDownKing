using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType{
    Equipable,
    Consumable
}

public enum ConsumableType{
    Health,
    Mana
}


[CreateAssetMenu(fileName = "Item", menuName = "NewData")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    public ConsumableItemData consumable;
    public GameObject equipPrefab;
}

public class ConsumableItemData
{
    public ConsumableType type;
    public float value;

}
