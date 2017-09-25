using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 卡牌类，这里处理卡牌的一些属性。
/// </summary>
public class Cards : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler{
    private GameObject character;
    private GameObject canvas;
    private Animator anim;
    // Use this for initialization
    void Start () {
        character = transform.GetChild(1).gameObject;

        character.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        anim = transform.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //指针移动到卡牌上的时候
    public void OnPointerEnter(PointerEventData eventData)
    {
        character.GetComponent<Image>().color = new Color(1,1,1,1);
       // transform.localScale = new Vector3(1.3f,1.3f,1.3f);
       
        //把Image放在最前面渲染，以便盖住两侧的Image
        transform.SetAsLastSibling();

        anim.Play("EnterCard");
        anim.speed = 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        character.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        anim.Play("ExitCard");
        anim.speed = 2;
    }

}
