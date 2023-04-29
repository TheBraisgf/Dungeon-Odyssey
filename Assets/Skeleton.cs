using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private void OnCollisionEnter2d(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<playerCombat>().getDmg(20, other.GetContact(0).normal);
        }
    }
}
