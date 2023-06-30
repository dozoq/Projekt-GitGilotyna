using UnityEngine;

namespace Code.Interaction
{
    public class DurabilityAction: MonoBehaviour
    {
        [SerializeField] private float durability = 1f;

        public void RemoveDurability(GameObject go)
        {
            durability -= 1f;
            if (durability <= 0)
            {
                Destroy(gameObject);
            }
        }


    }
}