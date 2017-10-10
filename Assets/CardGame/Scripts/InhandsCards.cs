using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来统一管理还没出的牌
/// </summary>
public class InhandsCards : MonoBehaviour {
    public static InhandsCards instance;

    public List<GameObject> cardsInHand;
    public float speed;
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// 出牌之后剩余的手牌自动缩进
    /// </summary>
    /// <param name="playedCard"></param>
    public void RegroupCards(GameObject playedCard)
    {
        
        //出的牌的前面的牌
        for (int i=0;i<cardsInHand.IndexOf(playedCard);i++)
        {
            cardsInHand[i].GetComponent<RectTransform>().position += new Vector3(130,0,0);
           // print("前面的牌是" + cardsInHand[i].transform.name);
        }
        //出的牌的后面的牌
        for (int j = cardsInHand.IndexOf(playedCard) +1;j<cardsInHand.Count;j++)
        {
            cardsInHand[j].GetComponent<RectTransform>().position -= new Vector3(130, 0, 0);
           // print("后面的牌是" + cardsInHand[j].transform.name);
        }
    }

}
