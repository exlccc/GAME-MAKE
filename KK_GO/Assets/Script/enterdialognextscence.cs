using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterdialognextscence : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
        if (collision.tag == "Player")
        {
            if (player.Gem >= 10 && player.destroy_Enemy >= 10)
            {
                enterDialog.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }
}
