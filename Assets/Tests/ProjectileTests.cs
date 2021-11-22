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
                firetest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TestFire"), new Vector3(0,0), Quaternion.identity);
                testFire = firetest.GetComponent<TestFire>();
            }

        

        [UnityTest]
        public IEnumerator CollisionTest()
        {   
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Obstacle"), new Vector2(6, 0), Quaternion.identity);
            GameObject projectile = testFire.spawnProjectile(new Vector2(1,0));
            yield return new WaitForSeconds(0.5f);
            UnityEngine.Assertions.Assert.IsNull(projectile);
        }

        [UnityTest]
        public IEnumerator MoveTest()
        {
            GameObject projectile = testFire.spawnProjectile(new Vector2(-1, 0  ));
            Vector3 pos = projectile.transform.position;
            // set projectile lifetimer higher thgan new wait for seconds.
            yield return new WaitForSeconds(0.5f);
            Vector2 newPos = projectile.transform.position;
            UnityEngine.Assertions.Assert.IsTrue(pos != projectile.transform.position);

        }
    }
}
