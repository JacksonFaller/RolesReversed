using UnityEngine;

public abstract class MoveInstruction : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            var controller = collision.gameObject.GetComponent<CharacterController>();
            DoAction(controller);
        }
    }

    protected abstract void DoAction(CharacterController controller);
}
