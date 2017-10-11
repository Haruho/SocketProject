using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manger : MonoBehaviour {
    public static Manger instance;
    //开局拥有的手牌数量
    [Tooltip("开局拥有的手牌数量")]
    public int mCardsNumber;

    public GameObject card;
    //卡牌的父物体  在画布下面
    public GameObject cardParent;
    //第一张卡牌的生成位置  下一张的卡牌生成位置是前一张卡牌的宽度 + 两张卡牌的间距
    public GameObject firstCardPos;
    //手里有的
    public List<GameObject> cardsInHand;
    //所有的卡片
    public List<Sprite> character;
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
    public void StartGame()
    {
        for (int i =0;i<mCardsNumber;i++)
        {
            int index = Random.Range(0,character.Count);
            //避免重复
            //List<int> allIndex = new List<int>();
            //if (allIndex.Contains(index))
            //{
            //    allIndex.Add();
            //}
            //else
            //{
            //    allIndex.Add(index);
            //}
            //生成卡牌
            GameObject go = Instantiate(card);
            //随即卡片
            go.transform.GetChild(1).GetComponent<Image>().sprite = character[index];
            //添加到手牌位置
            cardsInHand.Add(go);
            //位置不对 存疑
            go.GetComponent<RectTransform>().position = new Vector3(card.GetComponent<RectTransform>().rect.width * i + 280, 0, 0) + new Vector3(30 * i ,0 ,0);
            go.transform.SetParent(cardParent.transform,false);
        }
    }

    /// <summary>
    /// 出牌之后剩余的手牌自动缩进
    /// </summary>
    /// <param name="playedCard"></param>
    public void RegroupCards(GameObject playedCard)
    {

        //出的牌的前面的牌
        for (int i = 0; i < cardsInHand.IndexOf(playedCard); i++)
        {
            cardsInHand[i].GetComponent<RectTransform>().position += new Vector3(130, 0, 0);
            // print("前面的牌是" + cardsInHand[i].transform.name);
        }
        //出的牌的后面的牌
        for (int j = cardsInHand.IndexOf(playedCard) + 1; j < cardsInHand.Count; j++)
        {
            cardsInHand[j].GetComponent<RectTransform>().position -= new Vector3(130, 0, 0);
            // print("后面的牌是" + cardsInHand[j].transform.name);
        }
    }
}
