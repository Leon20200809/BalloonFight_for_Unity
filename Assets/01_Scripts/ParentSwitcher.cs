using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParentSwitcher : MonoBehaviour
{
    string player = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == player)
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == player)
        {
            col.transform.SetParent(null);
        }
    }
}
