using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// addressable 相关测试
public class AddressableTest : MonoBehaviour
{

    // public GameObject SpineGo;

    // public AssetReference SpineAsset;
    // Start is called before the first frame updates
    async void Start()
    {

        // GameObject go = await Addressables.InstantiateAsync("Assets/Resources_moved/Z_Test/InGame/Character/testrole/go.prefab", Vector3.zero, Quaternion.identity).Task;
        // Debug.Log(go);
        // Addressables.InstantiateAsync("Assets/Resources_moved/Z_Test/InGame/Character/ZhouYu/go.prefab",new Vector3(5,0,0), Quaternion.identity);
    }

    public void OnDestroy()
    {
        
    }
}
