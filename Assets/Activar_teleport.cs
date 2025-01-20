using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activar_teleport : MonoBehaviour
{
    [SerializeField]
    Totem teleport;
    [SerializeField]
    bool objecteNecesari;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPremut(){
        teleport.Activar();
    }

    void OnTriggerEnter(Collider other){
        print("Tocat");
        print(other.gameObject.tag);
        if ((other.gameObject.CompareTag("pes_boto") && objecteNecesari) ||
            (other.gameObject.CompareTag("Player") && !objecteNecesari))
        {
            teleport.Activar(); 
        }
    }

    void OnTriggerExit(Collider other){
        if ((other.gameObject.CompareTag("pes_boto") && objecteNecesari) ||
            (other.gameObject.CompareTag("Player") && !objecteNecesari))
        {
            teleport.Desactivar();
        }
    }
}
