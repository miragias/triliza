using UnityEngine;

namespace Gameplay.Systems.Interact
{
    [CreateAssetMenu(fileName = "NoInteractSystem", menuName = "Systems/NoInteractSystem", order = 0)]
    public class NoInteractSystem : ScriptableObject , IInteractSystem
    {
        public void Tick()
        {
            return;
        }
    }
}
