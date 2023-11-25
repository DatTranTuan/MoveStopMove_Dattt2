using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    public static JoystickManager Instance;

    public FixedJoystick _joystick;

    private void Awake()
    {
        Instance = this;
    }
}
