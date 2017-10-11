using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// 卡牌类，这里处理卡牌的一些属性。
/// </summary>
public class Cards : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler{
    public static string cardName;
    private GameObject character;
    private GameObject canvas;
    private Animator anim;
    private bool isPlayed;   //用来标记是不是已近出过的牌
    private GameObject introduction;    //卡牌的介绍窗口

    private GameObject outObject;       //通过改变介绍窗口的父物体实现隐藏和显示该窗口的功能
    // Use this for initialization
    void Start () {
        character = transform.GetChild(1).gameObject;

        character.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        anim = transform.GetComponent<Animator>();

        isPlayed = false;

        outObject = GameObject.FindGameObjectWithTag("object");
        introduction = GameObject.FindGameObjectWithTag("introduction");



    }
	
	// Update is called once per frame
	void Update () {
        if (isPlayed)
        {
            character.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
	}

    //指针移动到卡牌上的时候
    public void OnPointerEnter(PointerEventData eventData)
    {
        character.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        //把Image放在最前面渲染，以便盖住两侧的Image
        transform.SetAsLastSibling();

        anim.Play("EnterCard");
        anim.speed = 1.2f;

        introduction.transform.SetParent(canvas.transform, false);

        //跟随鼠标
        //   introduction.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(0,200,0);
        //悬浮在卡牌上面
        introduction.GetComponent<RectTransform>().position = this.transform.GetComponent<RectTransform>().position + new Vector3(0,410,0);
        //显示卡片信息
        CardInfo.instance.ShowInfoOnWindow(this.transform.GetChild(1).GetComponent<Image>().sprite.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        character.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        anim.Play("ExitCard");
        anim.speed = 2;

        introduction.transform.SetParent(outObject.transform,false);
    }
    //点击卡牌出牌
    public void OnPointerClick(PointerEventData eventData)
    {
        //anim.Play("playcard");
        isPlayed = true;
        transform.GetComponent<RectTransform>().position = new Vector2(950, 800);
        transform.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
        anim.enabled = false;
        //执行抵消空缺的函数
        Manger.instance.RegroupCards(this.gameObject);
        Manger.instance.cardsInHand.Remove(this.gameObject);
    }
}
