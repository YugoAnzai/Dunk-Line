using UnityEngine;

public class SimpleSceneLoader : MonoBehaviour
{

    public SceneLoader.Scene scene;

    public void ChangeToScene()
    {
        SceneLoader.Load(scene);
    }

}