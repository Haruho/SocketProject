using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverWindow : MonoBehaviour {
    public Canvas canvas;
    public RectTransform rectTransform;
    public float xOffset;
    public float yOffset;
    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

       // rectTransform = canvas.transform as RectTransform;
	}
	
	// Update is called once per frame
	void Update () {
        rectTransform.position = Input.mousePosition + new Vector3(xOffset,yOffset,0);
    }
}
