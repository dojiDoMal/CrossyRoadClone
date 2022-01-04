using SwordGC.AirController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevice : Device
{
    public PlayerDevice(int deviceId) : base(deviceId){

    }

    public override string View{
        get{
            return "gameplay";
        }
    }
}
