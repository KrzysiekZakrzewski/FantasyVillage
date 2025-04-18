using BlueRacconGames.InventorySystem;
using BlueRacconGames.Pool;
using Game.Item.Factory;
using Interactable;
using System.Collections;
using UnityEngine;
using Zenject;

public class ResourceInteractable : InteractablePooledBase
{
    [SerializeField] private InventoryUniqueId inventoryId;
    [SerializeField] private ItemFactorySO itemData;
    [SerializeField] private Vector2 minSpawnForce, maxSpawnForce;

    private Rigidbody2D rb;
    private InventoryController inventoryController;
    private MagnetInteractor magnetInteractor;

    private float minSpawnForceDuration = 0.4f;
    private float maxSpawnForceDuration = 0.5f;

    [Inject]
    private void Inject(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (magnetInteractor == null)
            magnetInteractor = GetComponent<MagnetInteractor>();
    }

    public override bool Interact(InteractorControllerBase interactor)
    {
        if (!IsInteractable)
            return false;

        bool result = inventoryController.AddItem(inventoryId, itemData);

        if (!result)
            return false;

        SwitchInteractable(false);

        Expire();

        return true;
    }

    public override void LeaveInteract(InteractorControllerBase interactor)
    {

    }

    public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition)
    {
        base.Launch(sourceEmitter, startPosition);

        LaunchItem();
    }

    protected override void Expire()
    {
        base.Expire();

        if (magnetInteractor == null) return;

        magnetInteractor.SwitchCanUse(false);
    }

    private void LaunchItem()
    {
        SwitchInteractable(false);

        rb.simulated = false;

        StartCoroutine(LaunchItemSequence());
    }

    private Vector2 RandomizeSpawnForce()
    {
        float spawnForceX = Random.Range(minSpawnForce.x, maxSpawnForce.x);
        float spawnForceY = Random.Range(minSpawnForce.y, maxSpawnForce.y);

        spawnForceX = Random.value > 0.5f ? spawnForceX : -spawnForceX;

        return new Vector2(spawnForceX, spawnForceY);
    }

    private IEnumerator LaunchItemSequence()
    {
        rb.simulated = true;

        rb.AddForce(RandomizeSpawnForce(), ForceMode2D.Impulse);

        yield return new WaitForSeconds(Random.Range(minSpawnForceDuration, maxSpawnForceDuration));

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        yield return null;

        SwitchInteractable(true);

        if (magnetInteractor == null) yield break;

        magnetInteractor.SwitchCanUse(true);
    }
}