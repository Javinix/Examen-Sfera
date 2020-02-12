using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instace;
    public User login;
    [SerializeField] private GameObject buttonStart;
    [SerializeField] private TextMeshProUGUI scoreMemorama;

    private void Awake()
    {
        instace = this;
    }
    public void GetUser(string _user)
    {
        login.user = _user;
    }

    public void GetPassword(string _password)
    {
        login.password = _password;
    }


    public void Confirm()
    {
        if (WebRequest.instance.Data.user.ToUpper() == this.login.user.ToUpper() && WebRequest.instance.Data.password == this.login.password)
            GetScene(1);
        else
            print("Usuario incorrecto");
    }

    public void StartSimon()
    {
        Cubes.instance.GetRobot();
        buttonStart.SetActive(false);
    }

    public void GetScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GetScoreMemorama(int score)
    {
        scoreMemorama.text = score.ToString();
    }
}
