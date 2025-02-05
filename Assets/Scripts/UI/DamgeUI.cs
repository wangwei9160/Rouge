using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamgeUI : MonoBehaviour 
{
    public float LifeTime = 0.5f;
    public float mCurrentSecond = 0f;

    public float Speed = 1.5f;
    public float ColorFadedSpeed = 20f;


    public TMP_Text damageUI;
    public GameObject critical;

    private Color curColor;

    void Start()
    {
        curColor = damageUI.color;
        float rdX = Random.Range(-0.2f, 0.2f);
        float rdY = Random.Range(-0.2f, 0.2f);
        transform.position = transform.position + new Vector3(rdX, rdY, 0);
    }

    void Update()
    {
        if(mCurrentSecond > LifeTime)
        {
            Destroy(gameObject);
            return;
        }
        mCurrentSecond += Time.deltaTime;
        transform.Translate(Speed * Time.deltaTime * Vector3.up);
        curColor.a -= ColorFadedSpeed * Time.deltaTime;
        damageUI.color = curColor;
    }

    // …Ë÷√Ã¯◊÷…À∫¶UI
    public void SetInfo(DamageData _data)
    {
        gameObject.transform.position = new Vector3(_data.pos.position.x, 2f, _data.pos.position.z);
        if(_data.Damage.isCritical)
        {
            Debug.Log("±©ª˜¡À" + ((int)_data.Damage.Value).ToString());
            critical.SetActive(true);
            damageUI.color = new Color(1f,0f,0f);
            curColor = damageUI.color;
        }
        damageUI.text = ((int)_data.Damage.Value).ToString();
    }
}