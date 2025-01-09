using TMPro;
using UnityEngine;

public class PlayerAttributeUI : MonoBehaviour 
{
    public TMP_Text hp;
    public TMP_Text attack;
    public TMP_Text speed;
    public TMP_Text defense;
    public TMP_Text magicDefense;
    public TMP_Text hitRate;
    public TMP_Text hitDmg;
    public TMP_Text lk;

    public void Refresh(GameData dt)
    {
        var attr = dt.playerAttr;
        hp.text = attr.�������ֵ.ToString();
        attack.text = attr.������.ToString();
        speed.text = attr.����.ToString();
        defense.text = attr.����.ToString();
        magicDefense.text = attr.����.ToString();
        hitRate.text = attr.������.ToString();
        hitDmg.text = attr.�����˺�.ToString();
        lk.text = attr.����.ToString();
    }

}