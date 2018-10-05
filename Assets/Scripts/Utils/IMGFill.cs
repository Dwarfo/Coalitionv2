using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IMGFill : MonoBehaviour {

    public Image fill;
    // Use this for initialization
    void Start ()
    {
        fill = gameObject.GetComponent<Image>();
        fill.type = Image.Type.Filled;
        fill.fillMethod = Image.FillMethod.Horizontal;
        fill.fillAmount = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
