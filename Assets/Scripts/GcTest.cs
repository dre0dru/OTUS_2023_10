using Unity.Profiling;
using UnityEngine;

public class GcTest : MonoBehaviour
{
    [SerializeField]
    private bool _withToString;

    private int _someInt;
    
    void Update()
    {
        var marker = new ProfilerMarker("ToStringCgTest");
        
        marker.Begin();
        if (_withToString)
        {
            for (int i = 0; i < 100; i++)
            {
                var stringValue = $"{_someInt.ToString()}";
            }
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {
                var stringValue = $"{_someInt}";
            }
        }
        marker.End();
    }
}
