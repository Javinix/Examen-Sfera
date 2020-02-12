using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public static CardSpawner instance;

    [SerializeField] private GameObject card;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float sumX, sumY;

    [SerializeField] private List<int> parejas = new List<int>();
    [SerializeField] private List<Card> cartas = new List<Card>();
    private List<Card> cardToOpen = new List<Card>();

    public bool canOpen;
    private int score;
    private void Start()
    {
        instance = this;
        canOpen = true;
        FillNumbers();
        CreateBoard();

        InvokeRepeating("CheckCards", 0, 0.8f);
    }

    void FillNumbers()
    {
        int counter = 0;
        bool r = false;
        for (int i = 0; i < 50; i++)
        {
            if (counter <= 50 / 2 && !r)
                counter++;
            if (counter > (50 / 2))
                r = true;
            if (r)
                counter--;

            parejas.Add(counter);
        }
    }

    void CreateBoard()
    {

        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                Vector2 pos = new Vector2((x * sumX) - offset.x, (y * sumY) - offset.y);
                GameObject o = Instantiate(card);
                o.transform.localPosition = pos;
                o.transform.SetParent(this.transform);

                Card c = o.GetComponent<Card>();

                if (c != null)
                {
                    int r = Random.Range(0, parejas.Count);
                    c.id = parejas[r];
                    parejas.RemoveAt(r);
                    cartas.Add(c);
                    c.SetNumber();
                    
                }
            }
        }

        for (int i = 0; i < cartas.Count; i++)
        {
            if (cartas[i].id == parejas[0])
                Destroy(cartas[i].gameObject);
        }
    }


    public void AddCart(Card _c)
    {
        if (cardToOpen.Count >= 2)
        {
            canOpen = false;
            return;
        }

        cardToOpen.Add(_c);

        if (cardToOpen.Count >= 2)
        {
            canOpen = false;
        }
    }

    public void RemoveCard(Card _c)
    {
        if (cardToOpen.Count < 1)
        {
            canOpen = true;
            return;
        }
        cardToOpen.Remove(_c);
    }
    void CheckCards()
    {
        if (cardToOpen.Count >= 2)
        {
            canOpen = false;
            Correct();
        }
        else
            canOpen = true;

    }


    void Correct()
    {
        if (cardToOpen[0].id == cardToOpen[1].id)
        {
            score++;
            UIController.instace.GetScoreMemorama(score);
            if (score >= 24)
                UIController.instace.GetScene(2);
            for (int i = 0; i < cardToOpen.Count; i++)
            {
                cardToOpen[i].alOpen = true;
                Destroy(cardToOpen[i].gameObject);
            }
            cardToOpen.Clear();
            canOpen = true;
        }
        else
        {
            for (int i = 0; i < cardToOpen.Count; i++)
                cardToOpen[i].Turn();
            cardToOpen.Clear();
            canOpen = true;
        }
    }
}
