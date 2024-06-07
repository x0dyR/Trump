//using UnityEngine;

//public class SkeletonSpawner : MonoBehaviour
//{
//    [SerializeField] private GameObject skeletonPrefab; // ������ �������
//    [SerializeField] private Transform[] spawnPoints;   // ������ ����� ������

//    void Start()
//    {
//        SpawnSkeletons();
//    }
//    public void SpawnSkeletons()
//    {
//        foreach (Transform spawnPoint in spawnPoints)
//        {
//            Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);
//        }
//    }

//}
using UnityEngine;
using Trump;
public class SkeletonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab; // ������ �������
    [SerializeField] private Transform[] spawnPoints;   // ������ ����� ������
    [SerializeField] private int maxOccupancyPerSkeleton = 1; // ������������ ���������� ����� ��������������, ������� ������ ������ ����� ������

    void Start()
    {
        SpawnSkeletons();
    }

    public void SpawnSkeletons()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject skeleton = Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);

            // ������������� ��� ������� ������� ������������ ���������� ����� ��������������
            Skelet skeletScript = skeleton.GetComponent<Skelet>();
            if (skeletScript != null)
            {
                skeletScript.MaxOccupancy = maxOccupancyPerSkeleton;
            }
        }
    }
}





