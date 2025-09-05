using UnityEngine;

public class DeployPointManager:MonoBehaviour
{
    public static DeployPointManager Instance { get; private set; }

    [SerializeField] private Transform deployPoint;
}
