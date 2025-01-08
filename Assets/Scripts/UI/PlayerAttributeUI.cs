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
        hp.text = attr.最大生命值.ToString();
        attack.text = attr.攻击力.ToString();
        speed.text = attr.攻速.ToString();
        defense.text = attr.防御.ToString();
        magicDefense.text = attr.法抗.ToString();
        hitRate.text = attr.暴击率.ToString();
        hitDmg.text = attr.暴击伤害.ToString();
        lk.text = attr.幸运.ToString();
    }

}