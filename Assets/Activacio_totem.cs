using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    bool activat;
    [SerializeField]
    private GameObject encen;
    [SerializeField]
    private GameObject apagat;
    [SerializeField]
    private GameObject snapVolume;

    [SerializeField]
    private InteractuableMagic interactuableMagic;

    // Start is called before the first frame update
    void Start()
    {
        activat = false;
        Desactivar();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activar()
    {
        interactuableMagic?.Activar();
        encen.SetActive(true);
        apagat.SetActive(false);
        activat = true;
        snapVolume.SetActive(true);
    }

    public void Desactivar()
    {
        interactuableMagic?.Desactivar();
        encen.SetActive(false);
        apagat.SetActive(true);
        activat = false;
        snapVolume.SetActive(false);
    }

    public bool get_Activat(){
        return activat;
    }
}
