using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cubes : MonoBehaviour
{
    public static Cubes instance;

    [SerializeField] private Color[] colors;
    [SerializeField] private Color[] originalColors;
    [SerializeField] private List<Cubo> cubos = new List<Cubo>();
    [SerializeField] private TextMeshProUGUI Score;

    private List<Cubo> robot = new List<Cubo>();
    private List<Cubo> userC = new List<Cubo>();

    int level;
    bool gameOver = false;
    public bool StartGame { get; set; }
    public int score { get; set; }

    private void Start()
    {
        instance = this;
        level = 1;

        for (int i = 0; i < cubos.Count; i++)
        {
            cubos[i].StartCube();
            cubos[i].id = i;
            cubos[i].colorH = colors[i];
            cubos[i].originalColor = originalColors[i];
            cubos[i].ChangeColor2();
        }

    }

    public void GetRobot()
    {
        for (int i = 0; i < level; i++)
        {
            int rand = Random.Range(0,cubos.Count);
            robot.Add(cubos[rand]);
        }
        StartCoroutine(GetColors());
    }

    public void AddCubo(Cubo cubo)
    {
        userC.Add(cubo);
    }


    IEnumerator GetColors()
    {
        for (int i = 0; i < robot.Count;i++)
        {
            yield return new WaitForSeconds(0.4f);
            robot[i].ChangeColor1();
            yield return new WaitForSeconds(1f);
            robot[i].ChangeColor2();
            
        }
        StartGame = true;
    }

    private void Update()
    {
        if(!gameOver)
            CheckSimon();
    }
    void CheckSimon()
    {
        if(userC.Count == robot.Count && StartGame)
        {
            StartGame = false;
            int counter = 0;
            for (int i = 0; i < robot.Count; i++)
            {
                if (userC[i].id == robot[i].id)
                    counter++;
            }

            if (counter == robot.Count)
            {
                print("Ganaste");
                level++;
                score += level;
                robot.Clear();
                userC.Clear();
                GetRobot();
                Score.text = score.ToString();
            }
            else
            {
                robot.Clear();
                userC.Clear();
                Score.text = "Perdiste";
                StartCoroutine(GetMainScene());
            }

        }

        if (!gameOver)
        {
            for (int i = 0; i < userC.Count; i++)
            {
                if (userC[i].id != robot[i].id)
                {
                    gameOver = true;
                    Score.text = "Perdiste";
                    StartCoroutine(GetMainScene());
                }
            }
        }
    }

    IEnumerator GetMainScene()
    {
        yield return new WaitForSeconds(3);
        UIController.instace.GetScene(0);
    }
}
