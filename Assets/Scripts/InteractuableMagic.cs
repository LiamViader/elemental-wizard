using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractuableMagic : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour grabable;

    [SerializeField]
    private Magic magicType;

    void OnEnable()
    {
        
    }

    // Cancelar la suscripción cuando el objeto se desactive
    void OnDisable()
    {
        WandController.Instance.MagicChanged -= OnMagicChanged;
    }

    // Start is called before the first frame update
    void Start()
    {

        WandController.Instance.MagicChanged += OnMagicChanged;
    }



    void OnMagicChanged(Magic newMagic)
    {
        if (newMagic == magicType)
        {
            grabable.enabled = true;
        }
        else {
            grabable.enabled = false;
        }
    }
}
