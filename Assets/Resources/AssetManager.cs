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

    public List<Sprite> itemSprite;             // ÎïÆ·Í¼Æ¬
    public List<Sprite> RankSprite;             // ÎäÆ÷µÈ¼¶
    public List<GameObject> WeaponPrefabs;      // ÎäÆ÷Ô¤ÖÆÌå
    public List<Sprite> WeaponForShowSprite;    // ÎäÆ÷Í¼Æ¬

    public GameObject SaveFileDataTpl;          // ´æµµÄ£°å
}