using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamgeUI : MonoBehaviour 
{
    public float LifeTime = 0.5f;
    public float mCurrentSecond = 0f;

    public float Speed = 1.5f;
    public float ColorFadedSpeed = 20f;


    public TMP_Text damageUI;

    private Color curColor;

    void Awake()
    {
    }

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

    // ���������˺�UI
    public void SetInfo(DamageData _data)
    {
        gameObject.transform.position = new Vector3(_data.pos.position.x, 2f, _data.pos.position.z);
        damageUI.text = _data.Damage.ToString();
    }
}