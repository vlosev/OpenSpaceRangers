using System;
using UnityEngine;

namespace Common.Unity.Test
{
    public class TestNotifiableMonobehaviour : NotifiableMonoBehaviour
    {
        private void Start()
        {
            var go1 = new GameObject("Test1", typeof(TestNotifiableMonobehaviourMessage));
            go1.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeToDispose(this);
            
            var go2 = new GameObject("Test2", typeof(TestNotifiableMonobehaviourMessage));
            go2.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeToDispose(this);

            var go3 = new GameObject("Test3", typeof(TestNotifiableMonobehaviourMessage));
            var subscribe1 = go3.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeToDispose(this);
            subscribe1.Dispose();
            
            var go4 = new GameObject("Test4", typeof(TestNotifiableMonobehaviourMessage));
            var subscribe2 = go4.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeToDispose(this);
            subscribe2.Dispose();

            Destroy(go1);
            go2.GetComponent<TestNotifiableMonobehaviourMessage>().Dispose();
            Destroy(go3);
            go4.GetComponent<TestNotifiableMonobehaviourMessage>().Dispose();
        }
    }
}