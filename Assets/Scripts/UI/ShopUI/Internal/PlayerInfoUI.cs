using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Slider LevelSlider;
    public Text CurLevel;
    public Text CurExp;

    public void UpdateExp(int level , int cExp , int mExp)
    {
        CurLevel.text = level.ToString();
        CurExp.text = string.Format("{0} / {1}", cExp, mExp);
        LevelSlider.value = 1.0f * cExp / mExp;
    }

}