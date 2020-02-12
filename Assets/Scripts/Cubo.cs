using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubo : MonoBehaviour
{
    public int id;

    public Color colorH;
    public Color originalColor;

    private SpriteRenderer rend;

    public void StartCube()
    {
        rend = GetComponent<SpriteRenderer>();
    }


    public void ChangeColor1()
    {
        rend.color = colorH;
    }

    public void ChangeColor2()
    {
        rend.color = originalColor;
    }


    private void OnMouseDown()
    {
        if(Cubes.instance.StartGame)
            Cubes.instance.AddCubo(this);
    }
}
