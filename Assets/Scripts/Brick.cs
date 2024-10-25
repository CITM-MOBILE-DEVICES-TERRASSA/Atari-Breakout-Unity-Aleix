using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int lives;
    private Collider2D brickCollider;
    bool isGhostMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(isGhostMode)
        //{

        //    brickCollider = GetComponent<Collider2D>();
        //    brickCollider.isTrigger = isGhostMode;
        //    //StartCoroutine(DisableGhostModeAfterTime(5f));
        //}
        //else
        //{
        //    brickCollider = GetComponent<Collider2D>();
        //    brickCollider.isTrigger = false;
        //}
    }

    //public void ToggleGhostMode()
    //{
        
    //    isGhostMode = true;
    //    Debug.Log("ghost");
        
    //}

    //private IEnumerator DisableGhostModeAfterTime(float time)
    //{
    //    yield return new WaitForSeconds(time); // Esperar el tiempo especificado

    //    // Desactivar el modo fantasma
    //    isGhostMode = false;
    //    brickCollider.isTrigger = false;
    //    Debug.Log("Modo Fantasma desactivado");
        
    //}
}
