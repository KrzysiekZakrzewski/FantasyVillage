using BlueRacconGames.Movement;
using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using BlueRacconGames.AI.Implementation;
using Pathfinding;
using System;
using UnityEngine;
using BlueRacconGames.Animation;

namespace BlueRacconGames.AI
{
    [RequireComponent(typeof(TopDownCharacterController2D))]
    public abstract class AIControllerBase : MonoBehaviour
    {
        protected IAIMode aIMode;

        [SerializeField]
        private Transform sensorPoint;
        [SerializeField]
        private LayerMask whatIsObstacle;
        [SerializeField]
        private float sensorRadius = 0.2f;

        private float speed = 10f;
        private int currentWaypointId;
        private EnemyAIDataBaseSO aIDataSO;
        private WonderAIMode wonderAI;
        private Seeker agent;
        private TopDownCharacterController2D characterController;
        private Path path;

        public Transform PlayerTransform { get; private set; }
        public bool IsStoped { get; private set; }

        public Action OnObstacleCollide;

        protected virtual void Update()
        {
            if (aIMode == null || wonderAI.isWondering) return;

            aIMode.Update();

            Move();
        }
        protected virtual void OnDestroy()
        {
            if (aIMode == null) return;

            aIMode.OnDestory();
        }

        public void TryChangeAIMode()
        {
            if (wonderAI.isWondering) return;

            wonderAI.SetupTimeTickSystem(OnStartWonder, OnEndWonder);
        }

        public void ForceChangeAIMode(IAIModeFactory modeFactory)
        {
            ChangeState(modeFactory);
        }

        public void UpdatePath(Vector3 target)
        {
            if (agent == null || !agent.IsDone()) return;

            agent.StartPath(characterController.Rb.position, target, OnPathCompleted);
            currentWaypointId = 0;
        }

        public void StartNavMeshAgent()
        {
            IsStoped = false;
        }

        public void StopNavMeshAgent()
        {
            IsStoped = true;
        }

        public void Initialize(EnemyAIDataBaseSO aIDataSO)
        {
            this.aIDataSO = aIDataSO;

            //PlayerTransform = FindAnyObjectByType<PlayerWarriorController>().transform;// TO DO Change this 

            agent = GetComponent<Seeker>();
            characterController = GetComponent<TopDownCharacterController2D>();

            wonderAI = new WonderAIMode();

            aIMode = aIDataSO.IdleAIModeOptions.CreateAIMode(this, aIDataSO);
        }
        protected virtual void OnStartWonder()
        {
            aIMode.OnStartWonder();

            NaturalBreaking();
        }

        protected virtual void OnEndWonder()
        {
            if (!aIMode.CanChangeMode(out var factory)) return;

            ChangeState(factory);
        }

        protected void Move()
        {
            if (path == null) return;

            if (currentWaypointId >= path.vectorPath.Count)
                return;

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypointId] - characterController.Rb.position).normalized;

            float force = speed * Time.deltaTime * direction.x;

            //characterController.Move(force, 0, false, false);

            float distance = Vector2.Distance(characterController.Rb.position, path.vectorPath[currentWaypointId]);

            if (distance < aIMode.ReachDestinationDistance)
                currentWaypointId++;

            FrontSensorListner();
        }

        protected void SetupAgentAI()
        {
            speed = aIMode.MoveSpeed;
        }

        protected void FrontSensorListner()
        {
            if (!Physics2D.OverlapCircle(sensorPoint.position, sensorRadius, whatIsObstacle)) return;

            OnObstacleCollide?.Invoke();
        }

        private void NaturalBreaking()
        {
            var currentPos = transform.position;

            Vector3 breakPos = currentPos + transform.forward;

            UpdatePath(breakPos);
        }

        private void ChangeState(IAIModeFactory modeFactory)
        {
            aIMode = modeFactory.CreateAIMode(this, aIDataSO);

            SetupAgentAI();
        }

        private void OnPathCompleted(Path path)
        {
            if (path.error) return;

            this.path = path;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(sensorPoint.position, sensorRadius);
        }
    }
}