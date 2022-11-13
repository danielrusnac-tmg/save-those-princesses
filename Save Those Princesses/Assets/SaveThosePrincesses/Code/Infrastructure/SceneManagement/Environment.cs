using UnityEngine;

namespace SaveThosePrincesses.Infrastructure.SceneManagement
{
    [CreateAssetMenu(menuName = "Save Those Princesses/Environment", fileName = "environment_")]
    public class Environment : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}