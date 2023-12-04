using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    public static JoystickManager Instance;

    public DynamicJoystick _joystick;

    private void Awake()
    {
        Instance = this;
    }
}
