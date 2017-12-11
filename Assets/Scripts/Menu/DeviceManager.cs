using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeviceManager : MonoBehaviour
{
    public static DeviceManager instance;
    public bool keyboardInUse = false;
    public bool[] devicesInUse;
    bool _devicesAllInUse = false;
    public bool devicesAllInUse
    {
        get
        {
            CheckDevices();
            return _devicesAllInUse;
        }
        private set
        {
            _devicesAllInUse = value;
        }
    }

	void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        devicesInUse = new bool[Input.GetJoystickNames().Length];

        for (int i = 0; i < devicesInUse.Length; i++)
            devicesInUse[i] = false;
    }

    void CheckDevices()
    {
        for (int i = 0; i < devicesInUse.Length; i++)
            if (devicesInUse[i] == false)
            {
                _devicesAllInUse = false;
                return;
            }

        _devicesAllInUse = true;
    }
}
