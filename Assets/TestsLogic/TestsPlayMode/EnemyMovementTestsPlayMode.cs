using System.Collections;
using _Project.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.Descriptors;

namespace TestsLogic.TestsPlayMode
{
    public class EnemyMovementTestsPlayMode
    {
        private GameObject _enemyObject;
        private Enemy _enemy;
        private EnemyMovement _enemyMovement;
        private GameObject _playerObject;
        private MainBuilding _mainBuilding;
        private NavMeshAgent _navMeshAgent;
        private GameObject _navMeshSurfaceObject;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            // Создание NavMeshSurface
            _navMeshSurfaceObject = new GameObject("NavMeshSurface");
            var navMeshSurface = _navMeshSurfaceObject.AddComponent<NavMeshSurface>();

            // Создание плоского объекта, чтобы создать NavMesh
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.transform.position = Vector3.zero;

            // Создание NavMesh
            navMeshSurface.BuildNavMesh();
            yield return null;

            // Создание объекта игрока
            _playerObject = new GameObject("Player");
            _playerObject.transform.position = new Vector3(0, 0, 10);

            // Создание главного здания
            var mainBuildingObject = new GameObject("MainBuilding");
            _mainBuilding = mainBuildingObject.AddComponent<MainBuilding>();
            _mainBuilding.transform.position = new Vector3(0, 0, -3);

            // Создание объекта врага и добавление необходимых компонентов
            _enemyObject = new GameObject("Enemy");
            _navMeshAgent = _enemyObject.AddComponent<NavMeshAgent>();
            _enemy = _enemyObject.AddComponent<Enemy>();

            // Инициализация врага
            var enemyDescriptor = ScriptableObject.CreateInstance<EnemyDescriptor>();
            enemyDescriptor.Health = 100;
            enemyDescriptor.MoveSpeed = 5f;
            enemyDescriptor.PursuitDistance = 15f;
            _enemy.Init(_playerObject, enemyDescriptor, _mainBuilding);

            // Обеспечиваем, что агент находится на NavMesh
            _navMeshAgent.enabled = false;
            yield return new WaitForFixedUpdate();

            NavMeshHit hit;
            if (NavMesh.SamplePosition(Vector3.zero, out hit, 10.0f, NavMesh.AllAreas))
            {
                _enemyObject.transform.position = hit.position;
            }
            else
            {
                Assert.Fail("Failed to place enemy on NavMesh.");
            }

            _navMeshAgent.enabled = true;
            yield return new WaitForFixedUpdate();

            // Добавление и инициализация компонента EnemyMovement после инициализации Enemy
            _enemyMovement = _enemyObject.AddComponent<EnemyMovement>();

            // Ждем до конца кадра, чтобы все компоненты инициализировались
            yield return new WaitForFixedUpdate();
        }

        [UnityTearDown]
        public IEnumerator Teardown()
        {
            Object.DestroyImmediate(_enemyObject);
            Object.DestroyImmediate(_playerObject);
            Object.DestroyImmediate(_mainBuilding.gameObject);
            Object.DestroyImmediate(_navMeshSurfaceObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator EnemyPursuesPlayerWhenInPursuitDistance()
        {
            // Проверка начального состояния
            Assert.IsFalse(_enemy.IsPursuingPlayer);
            Assert.IsFalse(_enemyMovement.EnemyCanAttack);

            // Перемещаем игрока в пределах дистанции преследования
            _playerObject.transform.position = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(1);

            // Проверка, что враг начал преследовать игрока
            Assert.IsTrue(_enemy.IsPursuingPlayer);
        }

        [UnityTest]
        public IEnumerator EnemyAttacksPlayerWhenCloseEnough()
        {
            // Перемещаем игрока в пределах дистанции преследования
            _playerObject.transform.position = new Vector3(0, 0, 1);
            yield return new WaitForSeconds(1);

            // Проверка, что враг может атаковать игрока
            Assert.IsTrue(_enemyMovement.EnemyCanAttack);
        }

        [UnityTest]
        public IEnumerator EnemyMovesToMainBuildingWhenPlayerIsOutOfRange()
        {
            // Проверка начального состояния
            Assert.IsFalse(_enemy.IsPursuingPlayer);
            
            // Перемещаем игрока за пределы дистанции преследования
            _playerObject.transform.position = new Vector3(0, 0, 20);
            yield return new WaitForSeconds(3);

            // Проверка, что враг начал двигаться к главному зданию
            Assert.IsFalse(_enemy.IsPursuingPlayer);
            Assert.AreEqual(_mainBuilding.transform.position.x, _enemyMovement.transform.position.x);
            Assert.AreEqual(_mainBuilding.transform.position.z, _enemyMovement.transform.position.z);
        }

        [UnityTest]
        public IEnumerator EnemyDisablesNavMeshAgentWhenAgentSetInactive()
        {
            // Проверка начального состояния
            Assert.IsTrue(_navMeshAgent.enabled);

            // Отключение NavMeshAgent
            _enemyMovement.AgentSetActive(false);
            yield return new WaitForSeconds(1);

            // Проверка, что NavMeshAgent отключен
            Assert.IsFalse(_navMeshAgent.enabled);
        }
    }
}
