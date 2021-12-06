using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CannonTowerTests
    {
        [UnityTest]
        public IEnumerator CannonTowerReloadTest()
        {
            GameObject go = new GameObject();
            CannonTower _cannonTower = go.AddComponent<CannonTower>();
            _cannonTower.Fire();
            Assert.Greater(_cannonTower.getWaitTime(), 0);
            yield return new WaitForSeconds(_cannonTower.getReloadTime() + 0.1f);
            Assert.LessOrEqual(_cannonTower.getWaitTime(), 0); 
        }

        [UnityTest]
        public IEnumerator CannonTowerFireTest()
        {
            GameObject go = new GameObject();
            CannonTower _cannonTower = go.AddComponent<CannonTower>();
            GameObject target = new GameObject();
            target.AddComponent<Rigidbody2D>();
            target.AddComponent<CircleCollider2D>();
            target.tag = "bloon";
            target.transform.position = new Vector3(0.5f, 0.5f);
            target.transform.position += new Vector3(-0.5f, -0.5f);

            yield return new WaitForSeconds(0.1f);

            Assert.Greater(_cannonTower.getWaitTime(), 0);
        }
    }
}