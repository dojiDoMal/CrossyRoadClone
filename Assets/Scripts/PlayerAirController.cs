using SwordGC.AirController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirController : AirController
{
    protected override Device GetNewDevice(int deviceId){
        return new PlayerDevice(deviceId);
    }
}
