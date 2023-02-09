namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        public bool IsFireButton() => UnityEngine.Input.GetMouseButton(0);

        public bool IsFireButtonUp() => UnityEngine.Input.GetMouseButtonUp(0);
    }
}