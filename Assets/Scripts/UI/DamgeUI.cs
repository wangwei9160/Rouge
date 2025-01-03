using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamgeUI : MonoBehaviour 
{
    public TMP_Text damgeUI;

    void Awake()
    {
    }

    void Start()
    {
        Destroy(gameObject , 0.5f);
    }

    void Update()
    {
    }

    // …Ë÷√Ã¯◊÷…À∫¶UI
    public void SetInfo(DamageData _data)
    {
        gameObject.transform.position = new Vector3(_data.pos.position.x, 2f, _data.pos.position.z);
        damgeUI.text = _data.Damage.ToString();
    }
}