using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CongelableManager : MonoBehaviour
{
    [SerializeField]
    private bool _congelat;

    [SerializeField]
    private GameObject _solidObject;
    [SerializeField]
    private GameObject _liquidObject;

    private void Awake(){
        UpdateState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateState(){
        _solidObject.SetActive(_congelat);
        _liquidObject.SetActive(!_congelat);
    }

    public void Congela(){
        _congelat =true;
        UpdateState();
    }

    public void Descongela(){
        _congelat=false;
        UpdateState();
    }
}
