using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public enum ItemType{
    Bullet,
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
    [PreviewField(100)]public GameObject dropPrefab;

    public ConsumableItemData consumable;
}
[System.Serializable]
public class ConsumableItemData
{
    public ConsumableType type;
    public float value;

}
