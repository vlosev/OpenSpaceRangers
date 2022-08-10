using System;
using UnityEngine;

namespace Common.Unity.Test
{
    public class TestNotifiableMonobehaviour : MonoBehaviour
    {
        private void Start()
        {
            var go1 = new GameObject("Test1", typeof(TestNotifiableMonobehaviourMessage));
            go1.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeOnDispose(() =>
            {
                Debug.Log($"Test destroy");
            });
            
            var go2 = new GameObject("Test2", typeof(TestNotifiableMonobehaviourMessage));
            go2.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeOnDispose(() =>
            {
                Debug.Log($"Test dispose");
            });

            var go3 = new GameObject("Test3", typeof(TestNotifiableMonobehaviourMessage));
            var subscribe1 = go3.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeOnDispose(() =>
            {
                Debug.Log($"Test destroy unsubscribed");
            });
            subscribe1.Dispose();
            
            var go4 = new GameObject("Test4", typeof(TestNotifiableMonobehaviourMessage));
            var subscribe2 = go4.GetComponent<TestNotifiableMonobehaviourMessage>().SubscribeOnDispose(() =>
            {
                Debug.Log($"Test dispose unsubscribed");
            });
            subscribe2.Dispose();

            Destroy(go1);
            go2.GetComponent<TestNotifiableMonobehaviourMessage>().Dispose();
            Destroy(go3);
            go4.GetComponent<TestNotifiableMonobehaviourMessage>().Dispose();
        }
    }
}