using System.Collections.Generic;
using UnityEngine;

public class HapticSingleton : MonoBehaviour
{
    public enum Contol { left, right }


    public static HapticSingleton Instance { get; private set; }

    List<UnityEngine.XR.InputDevice> leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
    List<UnityEngine.XR.InputDevice> rightHandedControllers = new List<UnityEngine.XR.InputDevice>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update(){
        if (leftHandedControllers.Count <= 0){
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
            HapticImpulse(Contol.left, 0.5f, 1f);
        }

        if (rightHandedControllers.Count <= 0)
        {
            var rdesiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rdesiredCharacteristics, rightHandedControllers);
            HapticImpulse(Contol.right, 0.5f, 1f);
        }
    }

    public void HapticImpulse (HapticSingleton.Contol control, float amplitude, float duration) 
    {
        switch(control){
            case Contol.left: 
                foreach (UnityEngine.XR.InputDevice device in leftHandedControllers)
                {
                    UnityEngine.XR.HapticCapabilities capabilities;
                    if (device.TryGetHapticCapabilities(out capabilities))
                    {
                        if (capabilities.supportsImpulse)
                        {
                            uint channel = 0;
                            device.SendHapticImpulse(channel, amplitude, duration);
                        }
                    }
                }
                break;
            case Contol.right:
                foreach (UnityEngine.XR.InputDevice device in rightHandedControllers)
                {
                    UnityEngine.XR.HapticCapabilities capabilities;
                    if (device.TryGetHapticCapabilities(out capabilities))
                    {
                        if (capabilities.supportsImpulse)
                        {
                            uint channel = 0;
                            device.SendHapticImpulse(channel, amplitude, duration);
                        }
                    }
                }
                break;
            default:
                Debug.LogError("Incorrect device");
                break;
        }
    }
}
