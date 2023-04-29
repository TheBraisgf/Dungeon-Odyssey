using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [SerializeField] private float vida;
    private Movement Movement;
    [SerializeField] private float tiempoPerdidaControl;

    private Animator animator;

    private void Start(){
        Movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }

    public void getDmg(float dmg){
        vida -= dmg;
    }

    public void getDmg(float dmg, Vector2 posicion){
        vida -= dmg;
        animator.SetTrigger("Golpe");
      StartCoroutine(PerderControl());
        Movement.Rebote(posicion);
    }

    private IEnumerator PerderControl(){
        Movement.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Movement.sePuedeMover = true;
    }
}
