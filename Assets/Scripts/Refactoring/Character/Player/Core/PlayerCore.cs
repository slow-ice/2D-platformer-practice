using Assets.Scripts.Refactoring.Architecture;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : IController
{
    
    public PlayerCore InitCore() {
        return this;
    }

    public IArchitecture GetArchitecture() {
        return PlatformerArc.Interface;
    }
}
