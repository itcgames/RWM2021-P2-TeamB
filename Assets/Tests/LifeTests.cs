using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LifeTests
    {
            private GameObject lifeSystem;
            Life lifeComponent;
            // A Test behaves as an ordinary method
            [SetUp]
            public void SetUp()
            {
               lifeSystem = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LifeSystem"));
               lifeComponent = lifeSystem.GetComponent<Life>();
            }

            [UnityTest]
            public IEnumerator ReduceLife()
            {        
                lifeComponent.removeLife(23);
                yield return new WaitForSeconds(.5f);
                UnityEngine.Assertions.Assert.IsTrue(lifeComponent.currentLives < lifeComponent.maxLives);
            }

            [UnityTest]
            public IEnumerator UpdateLifeTotal()
            {        
                string text = lifeComponent.lifeText.text;
                lifeComponent.removeLife(2);
                yield return new WaitForSeconds(.5f);
                UnityEngine.Assertions.Assert.IsFalse(text == lifeComponent.lifeText.text);
            }
        }
}