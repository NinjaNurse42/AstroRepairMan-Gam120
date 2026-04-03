using UnityEngine;

public class ScrapPickUp : MonoBehaviour
{
    [SerializeField] public int partsAmount = 1;

    // Delay after spawn before this pickup can be collected (prevents immediate re-collection on death)
    [SerializeField] private float pickupDelay = 0.5f;

    // Guard to prevent the pickup from being processed more than once
    // (multiple colliders / nested triggers can cause duplicate OnTriggerEnter2D calls).
    private bool _processing;

    // Whether the pickup is allowed to be collected
    private bool _canBePickedUp;

    private Collider2D[] _colliders;
    private SpriteRenderer[] _sprites;

    void Awake()
    {
        // Cache components
        _colliders = GetComponents<Collider2D>();
        _sprites = GetComponentsInChildren<SpriteRenderer>();

        // Immediately disable colliders so the spawner/overlapping player cannot pick it up in the same frame
        if (_colliders != null)
        {
            foreach (var c in _colliders)
            {
                if (c != null) c.enabled = false;
            }
        }

        // Start delay then enable pickup
        StartCoroutine(EnablePickupAfterDelay());
    }

    private System.Collections.IEnumerator EnablePickupAfterDelay()
    {
        _canBePickedUp = false;
        yield return new WaitForSeconds(pickupDelay);

        if (_colliders != null)
        {
            foreach (var c in _colliders)
            {
                if (c != null) c.enabled = true;
            }
        }

        _canBePickedUp = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canBePickedUp) return;
        if (_processing) return;
        _processing = true;

        // support PlayerInventory on collider or on a parent (handles nested player setups)
        PlayerInventory inventory = other.GetComponent<PlayerInventory>() ?? other.GetComponentInParent<PlayerInventory>();
        if (inventory == null)
        {
            _processing = false;
            return;
        }

        // Prevent double-trigger: disable this pickup's colliders/visual immediately
        if (_colliders != null)
        {
            foreach (var c in _colliders)
            {
                if (c != null) c.enabled = false;
            }
        }

        if (_sprites != null)
        {
            foreach (var s in _sprites)
            {
                if (s != null) s.enabled = false;
            }
        }

        bool added = inventory.AddParts(partsAmount);

        if (added)
        {
            Destroy(gameObject);
        }
        else
        {
            // Player is full — re-enable so pickup can be collected later
            if (_colliders != null)
            {
                foreach (var c in _colliders)
                {
                    if (c != null) c.enabled = true;
                }
            }

            if (_sprites != null)
            {
                foreach (var s in _sprites)
                {
                    if (s != null) s.enabled = true;
                }
            }

            _processing = false;
            Debug.Log("ScrapPickUp: player at max scrap, leaving pickup in world", this);
        }
    }
}


