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
            Debug.Log(_baseTower);
            _baseTower.Fire();
            Assert.Greater(_baseTower.getWaitTime(), 0);
            yield return new WaitForSeconds(_baseTower._reloadTime + 0.1f);
            Assert.LessOrEqual(_baseTower.getWaitTime(), 0); 
        }
    }
}
