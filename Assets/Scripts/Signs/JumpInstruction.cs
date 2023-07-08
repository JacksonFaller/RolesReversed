using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    public class JumpInstruction : MoveInstruction
    {
        protected override void DoAction(CharacterController controller)
        {
            controller.Jump();
        }
    }
}
