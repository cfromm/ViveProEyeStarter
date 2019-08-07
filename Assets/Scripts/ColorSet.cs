using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSet : MonoBehaviour {
    public Renderer rend;
    // Use this for initialization
    public Color newColor;
	void Start () {
        
        rend = GetComponent<Renderer>();
        //string[] hsvl = Experiment.BackgroundColor.Split(',');
        //Debug.Log(hsvl);
        //newColor = new Color( float.Parse(hsvl[0]), float.Parse(hsvl[1]), float.Parse(hsvl[2]), float.Parse(hsvl[3]));
        rend.material.color = newColor;
        //Debug.Log("BackgroundColor" + newColor);
    }

}
