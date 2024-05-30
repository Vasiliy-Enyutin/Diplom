using NUnit.Framework;
using UnityEngine;
using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic.InventoryLogic;

namespace TestsLogic.TestsEditMode
{
	public class PlayerInventoryControllerTestsEditMode : MonoBehaviour
	{
		private InventoryController _inventoryController;

        [SetUp]
        public void Setup()
        {
            GameObject gameObject = new GameObject();
            _inventoryController = gameObject.AddComponent<InventoryController>();
        }

        [TearDown]
        public void Teardown()
        {
            DestroyImmediate(_inventoryController.gameObject);
        }

        [Test]
        public void AddResourceIncreasesResourceAmount()
        {
            bool eventCalled = false;
            ResourceType eventResourceType = default;
            int eventAmount = 0;

            _inventoryController.OnResourceAmountChanged += (resourceType, amount) =>
            {
                eventCalled = true;
                eventResourceType = resourceType;
                eventAmount = amount;
            };

            _inventoryController.AddResource(ResourceType.Coal, 10);

            int amount = _inventoryController.GetSpecifiedResourceAmountOrLess(ResourceType.Coal, 10);

            Assert.IsTrue(eventCalled);
            Assert.AreEqual(ResourceType.Coal, eventResourceType);
            Assert.AreEqual(-10, eventAmount);
            Assert.AreEqual(10, amount);
        }

        [Test]
        public void GetSpecifiedResourceAmountOrLessDecreasesResourceAmount()
        {
            _inventoryController.AddResource(ResourceType.Coal, 10);

            bool eventCalled = false;
            ResourceType eventResourceType = default;
            int eventAmount = 0;

            _inventoryController.OnResourceAmountChanged += (resourceType, amount) =>
            {
                eventCalled = true;
                eventResourceType = resourceType;
                eventAmount = amount;
            };

            int receivedAmount = _inventoryController.GetSpecifiedResourceAmountOrLess(ResourceType.Coal, 5);

            Assert.IsTrue(eventCalled);
            Assert.AreEqual(ResourceType.Coal, eventResourceType);
            Assert.AreEqual(-5, eventAmount);
            Assert.AreEqual(5, receivedAmount);
        }

        [Test]
        public void GetBulletsNumberThatCanBeCreatedReturnsMinimumResourceAmount()
        {
            _inventoryController.AddResource(ResourceType.Coal, 10);
            _inventoryController.AddResource(ResourceType.Lead, 5);
            _inventoryController.AddResource(ResourceType.Sulfur, 7);

            bool eventCalled = false;
            int eventCallCount = 0;

            _inventoryController.OnResourceAmountChanged += (resourceType, amount) =>
            {
                eventCalled = true;
                eventCallCount++;
            };

            int bulletsCanBeCreated = _inventoryController.GetBulletsNumberThatCanBeCreated();

            Assert.IsTrue(eventCalled);
            Assert.AreEqual(5, bulletsCanBeCreated);
            Assert.AreEqual(3, eventCallCount);
        }
	}
}
