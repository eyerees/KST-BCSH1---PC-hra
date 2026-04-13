public interface IInteractable
{
    void Interact(System.Action onFinished);
    bool CanInteract();
}