using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI number;

    public int id;
    private bool open;
    public bool alOpen; 

    public void SetNumber()
    {
        alOpen = false;
        open = false;
        number.transform.gameObject.SetActive(false);

        number.text = id.ToString();
    }

    private void OnMouseDown()
    {
        if (alOpen)
            return;

        open = !open;
        if (CardSpawner.instance.canOpen)
        {

            if (open)
            {
                number.transform.gameObject.SetActive(true);
                CardSpawner.instance.AddCart(this);
            }
        }
        if (!open)
        {
            number.transform.gameObject.SetActive(false);
            CardSpawner.instance.RemoveCard(this);
        }

    }

    public void Turn()
    {
        number.transform.gameObject.SetActive(false);
        open = false;
    }
}
