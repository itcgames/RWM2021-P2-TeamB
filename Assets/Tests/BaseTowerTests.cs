using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BaseTowerTests
    {
        [UnityTest]
        public IEnumerator BaseTowerReloadTest()
        {
            GameObject go = new GameObject();
            BaseTower _baseTower = go.AddComponent<BaseTower>();
            _baseTower.Fire();
            Assert.Greater(_baseTower.getWaitTime(), 0);
            yield return new WaitForSeconds(_baseTower.getReloadTime() + 0.1f);
            Assert.LessOrEqual(_baseTower.getWaitTime(), 0); 
        }

        [UnityTest]
        public IEnumerator BaseTowerFireTest()
        {
            GameObject go = new GameObject();
            go.AddComponent<TargetingSystem>();
            BaseTower _baseTower = go.AddComponent<BaseTower>();
            GameObject target = new GameObject();
            target.AddComponent<Rigidbody2D>();
            target.AddComponent<CircleCollider2D>();
            target.transform.position = new Vector3(0.5f, 0.5f);
            target.transform.position += new Vector3(-0.5f, -0.5f);

            yield return new WaitForSeconds(0.1f);

            Assert.Greater(_baseTower.getWaitTime(), 0);
        }
    }
}
