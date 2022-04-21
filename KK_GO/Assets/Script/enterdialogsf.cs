using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterdialogsf : MonoBehaviour
{
    public GameObject enterDialogs, enterDialogf;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
        if (collision.tag == "Player")
        {
            if (player.Gem < 20)
            {
                enterDialogf.SetActive(true);
            }
            else
            {
                enterDialogs.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialogs.SetActive(false);
            enterDialogf.SetActive(false);
        }
    }
}
