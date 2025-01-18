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

    public List<Sprite> itemSprite;             // ��ƷͼƬ
    public List<Sprite> RankSprite;             // �����ȼ�
    public List<GameObject> WeaponPrefabs;      // ����Ԥ����
    public List<Sprite> WeaponForShowSprite;    // ����ͼƬ

    public GameObject SaveFileDataTpl;          // �浵ģ��
}