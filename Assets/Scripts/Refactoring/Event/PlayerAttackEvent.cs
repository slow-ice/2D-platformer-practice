using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvent
{
    public int attackIndex;

    public PlayerAttackEvent(int attackIndex) {
        this.attackIndex = attackIndex;
    }
}
