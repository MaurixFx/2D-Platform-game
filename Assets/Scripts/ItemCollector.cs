using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherrysCount = 0;
    [SerializeField] private Text counterText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            cherrysCount++;
            counterText.text = "Cherries: " + cherrysCount;
            Destroy(collision.gameObject); // We destroy the object who the player collides, in this case the cherry
        }
    }
}
