using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ProjectileTests
    {
        private GameObject firetest;
        private TestFire testFire;
            // A Test behaves as an ordinary method
            [SetUp]
            public void SetUp()
            {
                GameObject firetest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TestFire"));
                testFire = firetest.GetComponent<TestFire>();
            }

            [UnityTest]
            public IEnumerator MoveTest()
            {        
                testFire.spawnProjectile(new Vector2(1,1));
                GameObject projectile = testFire.getprojectile();
                yield return new WaitForSeconds(1.1f);
                UnityEngine.Assertions.Assert.IsNull(projectile);
            }

            [UnityTest]
            public IEnumerator CollisionTest()
            {   
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Obstacle"), new Vector2(3, 0), Quaternion.identity);
                testFire.spawnProjectile(new Vector2(1,0));
                GameObject projectile = testFire.getprojectile();
                yield return new WaitForSeconds(0.5f);
                UnityEngine.Assertions.Assert.IsNull(projectile);
            }
        }
}
