using UnityEngine;

public class ArrivalObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public class ArrivalObjective : MonoBehaviour
    {
        [SerializeField] DialougeObject onArriveDialogue;
        [SerializeField] bool oneShot = true;
        [SerializeField] string playerTag = "Player";

        bool completed;

        void Awake()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (completed && oneShot) return;
            if (!other.CompareTag(playerTag)) return;

            if (onArriveDialogue != null)
            {
                DialougeManager.Show(onArriveDialogue);
            }

            if (oneShot)
                completed = true;
        }
    }
}
