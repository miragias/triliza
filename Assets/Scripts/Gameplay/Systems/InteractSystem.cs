using UnityEngine;

namespace Gameplay.Systems.Interact
{
    [CreateAssetMenu(fileName = "InteractSystem", menuName = "Systems/InteractSystem", order = 0)]
    public class InteractSystem : ScriptableObject , IInteractSystem
    {
        private Camera m_InteractCamera;

        public void SetupSystem(Camera cam)
        {
            m_InteractCamera = cam;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(m_InteractCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    IInteractable interactableFound = hit.collider.gameObject.GetComponentInChildren<IInteractable>();
                    if(interactableFound != null)
                    {
                        interactableFound.Interact();
                    }
                }
            }
        }
    }
}
