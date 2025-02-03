using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Slider LevelSlider;
    public Text CurLevel;
    public Text CurExp;

    public Slider HpSlider;
    public Slider LowHpSlider;
    public Text CurHp;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopUI , Func);
    }

    private void OnDestroy()
    {   
        EventCenter.RemoveListener(EventDefine.ShowShopUI, Func);
    }

    private void Update()
    {
        if(HpSlider.value < LowHpSlider.value)
        {
            LowHpSlider.value -= Time.deltaTime * 0.1f;
        }else
        {
            Func();
        }
    }

    public void Func()
    {
        LowHpSlider.value = HpSlider.value;
    }

    public void UpdateExp(int level , int cExp , int mExp)
    {
        CurLevel.text = level.ToString();
        CurExp.text = string.Format("{0} / {1}", cExp, mExp);
        LevelSlider.value = 1.0f * cExp / mExp;
    }

    public void UpdateHp(int cHp , int mHp)
    {
        CurHp.text = string.Format("{0} / {1}", cHp, mHp);
        HpSlider.value = 1.0f * cHp / mHp;
    }

}