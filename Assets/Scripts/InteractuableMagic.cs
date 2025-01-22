using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractuableMagic : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> controledByMagicList;

    [SerializeField]
    private List<Collider> controledByMagicListColliders;

    [SerializeField]
    private List<Magic> magicTypes;

    private Magic lastMagicSelected=Magic.Terra;

    private bool activat = true;

    void OnEnable()
    {

    }

    void OnDisable()
    {
        //WandController.Instance.MagicChanged -= OnMagicChanged;
    }


    void Start()
    {
        WandController.Instance.MagicChanged += OnMagicChanged;

    }

    public void AddMagicType(Magic magictype)
    {
        magicTypes.Add(magictype);
    }

    public void RemoveMagicType(Magic magictype)
    {
        if (magicTypes.Contains(magictype))
        {
            magicTypes.Remove(magictype);
        }
    }

    public bool IsInfluencedByMagic(Magic magictype)
    {
        return magicTypes.Contains(magictype);
    }

    public void Desactivar()
    {
        activat = false;
    }

    public void Activar()
    {
        activat = true;
        OnMagicChanged(lastMagicSelected);
    }

    public bool CanInteract(Magic selectedMagic)
    {
        return activat && IsInfluencedByMagic(selectedMagic);
    }

    void OnMagicChanged(Magic newMagic)
    {
        lastMagicSelected = newMagic;
        if (activat)
        {
            foreach (var monoBehaviour in controledByMagicList)
            {
                if (IsInfluencedByMagic(newMagic))
                {
                    monoBehaviour.enabled = true;
                }
                else
                {
                    monoBehaviour.enabled = false;
                }
            }
            foreach (var monoBehaviour in controledByMagicListColliders)
            {
                if (IsInfluencedByMagic(newMagic))
                {
                    monoBehaviour.enabled = true;
                }
                else
                {
                    monoBehaviour.enabled = false;
                }
            }
        }

    }
}
