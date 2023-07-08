using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInstruction : MoveInstruction
{
    protected override void DoAction(CharacterController controller)
    {
        controller.Stop();
    }
}
