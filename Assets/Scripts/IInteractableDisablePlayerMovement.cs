using System;

public interface IInteractableDisablePlayerMovement : IInteractable
{
    void DisablePlayerMovement(Action releasePlayerMovementCallback);
}
