using BlueRacconGames.Inventory;
using Game.Item.Factory;
using Interactable;
using System.Collections;
using UnityEngine;
using Zenject;

public class ResourceInteractable : InteractableBase
{
    [SerializeField] private ItemFactorySO itemData;
    [SerializeField] private Vector2 minSpawnForce, maxSpawnForce;

    private float spawnPosY;

    private Rigidbody2D rb;
    private InventoryManager inventoryManager;

    [Inject]
    private void Inject(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }

    private void Awake()
    {
        LunchItem();
    }

    public override bool Interact(InteractorControllerBase interactor)
    {
        if (!IsInteractable)
            return false;

        var result = inventoryManager.PlayerInventory.AddItem(itemData);

        if (!result)
            return false;

        SwitchInteractable(false);

        Destroy(gameObject, 0.1f);

        return true;
    }

    public override void LeaveInteract(InteractorControllerBase interactor)
    {

    }

    private void LunchItem()
    {
        SwitchInteractable(false);

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        spawnPosY = rb.position.y;
        rb.simulated = false;

        StartCoroutine(LunchSequence());
    }

    private Vector2 RandomizeSpawnForce()
    {
        float spawnForceX = Random.Range(minSpawnForce.x, maxSpawnForce.x);
        float spawnForceY = Random.Range(minSpawnForce.y, maxSpawnForce.y);

        spawnForceX = Random.value > 0.5f ? spawnForceX : -spawnForceX;

        return new Vector2(spawnForceX, spawnForceY);
    }

    private IEnumerator LunchSequence()
    {
        rb.simulated = true;

        rb.AddForce(RandomizeSpawnForce(), ForceMode2D.Impulse);

        yield return new WaitUntil(() => rb.position.y < spawnPosY);

        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;

        SwitchInteractable(true);
    }
}
