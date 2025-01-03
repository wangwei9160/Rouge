using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LargeBoardUI : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }
}
