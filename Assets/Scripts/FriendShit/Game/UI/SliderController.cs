using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider _slider;

    [SerializeField]
    private Text _value;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();        
    }

    // Update is called once per frame
    void Update()
    {
        int sensitivity = (int)(_slider.value * 25f);
        if(sensitivity == 0)
            sensitivity = 1;
        _value.text = sensitivity.ToString();
    }
}
