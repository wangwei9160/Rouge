using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName ="AssetManager")]
public class AssetManager : ScriptableObject 
{
    private static AssetManager instance;

    public static AssetManager Instance
    {
        get
        {
            if (instance == null) instance = Resources.Load<AssetManager>("AssetManager");
            return instance;
        }
    }

    public List<Sprite> itemSprite;
    public List<Sprite> RankSprite;
    public List<GameObject> WeaponPrefabs;
    public List<Sprite> WeaponForShowSprite;
}