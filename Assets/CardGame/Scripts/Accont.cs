using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 用户类，这里处理游戏账户拥有的卡牌，并且和服务器端交互
/// </summary>
public class Accont : MonoBehaviour {
    public InputField userName;
    public InputField password;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LogIn()
    {
        if (userName.textComponent.text == "test" && password.textComponent.text == "test")
        {
            SceneManager.LoadScene(1);
        }
    }
}
