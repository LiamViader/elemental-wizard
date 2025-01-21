using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public enum Magic
{
    Aire,
    Gel, 
    Foc,
    Terra
}


public class WandController : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer orbMesh;

    public static WandController Instance { get; private set; }

    private int _currentIndex;

    [SerializeField]
    private List<Magic> magicList;

    [SerializeField]
    private List<Material> magicMaterial;



    [SerializeField]
    [Tooltip("The Input System Action that will be used to perform the magic swap. Must be a Button Control.")]
    InputActionProperty m_MagicSwapAction = new InputActionProperty(new InputAction("Grab Move", type: InputActionType.Button));

    [SerializeField]
    [Tooltip("The Input System Action that will be used to perform the magic swap. Must be a Button Control.")]
    InputActionProperty m_CongelarDescongelarAction = new InputActionProperty(new InputAction("Grab Move", type: InputActionType.Button));

    [SerializeField]
    private ActionBasedController rightController;

    
    public event System.Action<Magic> MagicChanged;

    
    private void Awake()
    {
        // Assegura que només hi ha una instància del singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Múltiples instàncies de SingletonExample detectades. Destruint la nova.");
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    void Start()
    {

        _currentIndex = 0;

        m_MagicSwapAction.action.Enable();

        m_MagicSwapAction.action.performed += ctx => PerformMagicSwap();

        m_CongelarDescongelarAction.action.Enable();
        m_CongelarDescongelarAction.action.performed += ctx => PerformCongelarDescongelar();

        StartCoroutine(DelayedMethod());
    }

    IEnumerator DelayedMethod()
    {
        yield return new WaitForSeconds(0.1f);

        UpdateMagicSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformMagicSwap()
    {
        RotateMagic();
        UpdateMagicSelected();
        UpdateOrbMaterial();
    }

    public void PerformCongelarDescongelar(){
        if (magicList[_currentIndex] == Magic.Gel || magicList[_currentIndex] == Magic.Foc){
            Ray ray = new Ray(rightController.transform.position, rightController.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 10000f))
            {
                CongelableManager congelable = hit.collider.attachedRigidbody?.GetComponent<CongelableManager>();

                if (congelable != null)
                {
                    if (magicList[_currentIndex] == Magic.Gel)
                    {
                        congelable.Congela();
                    }
                    else if (magicList[_currentIndex] == Magic.Foc)
                    {
                        congelable.Descongela();
                    }
                }
            }
        }
        
    }

    private void UpdateMagicSelected(){
        switch (magicList[_currentIndex])
        {
            case Magic.Aire:
                Debug.Log("Aire");
                break;

            case Magic.Gel:
                Debug.Log("Lanzando hechizo de Gel: Congela un área a su alrededor.");
                break;

            case Magic.Foc:
                Debug.Log("Lanzando hechizo de Fuego: Dispara una bola de fuego.");
                break;

            case Magic.Terra:
                Debug.Log("Terra");
                break;
        }
        MagicChanged?.Invoke(magicList[_currentIndex]);
    }

    private void UpdateOrbMaterial()
    {
        if (orbMesh != null && _currentIndex >= 0 && _currentIndex < magicMaterial.Count)
        {
            orbMesh.material = magicMaterial[_currentIndex];
        }
        else
        {
            Debug.LogWarning("El material no se pudo cambiar, asegúrate de que orbMesh y magicMaterial están configurados correctamente.");
        }
    }

    public void RotateMagic()
    {

        int newIndex = (_currentIndex + 1) % magicList.Count;

        if (newIndex < 0)
        {
            newIndex = magicList.Count - 1;
        }

        _currentIndex = newIndex;
    }

    void OnDestroy()
    {
        m_MagicSwapAction.action.Disable();
        m_MagicSwapAction.action.performed -= ctx => PerformMagicSwap();

        m_CongelarDescongelarAction.action.Disable();
        m_CongelarDescongelarAction.action.performed -= ctx => PerformCongelarDescongelar();
    }
}
