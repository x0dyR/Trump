//using UnityEngine;

//public class SkeletonSpawner : MonoBehaviour
//{
//    [SerializeField] private GameObject skeletonPrefab; // Префаб скелета
//    [SerializeField] private Transform[] spawnPoints;   // Массив точек спавна

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
    [SerializeField] private GameObject skeletonPrefab; // Префаб скелета
    [SerializeField] private Transform[] spawnPoints;   // Массив точек спавна
    [SerializeField] private int maxOccupancyPerSkeleton = 1; // Максимальное количество точек патрулирования, которые каждый скелет может занять

    void Start()
    {
        SpawnSkeletons();
    }

    public void SpawnSkeletons()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject skeleton = Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);

            // Устанавливаем для каждого скелета максимальное количество точек патрулирования
            Skelet skeletScript = skeleton.GetComponent<Skelet>();
            if (skeletScript != null)
            {
                skeletScript.MaxOccupancy = maxOccupancyPerSkeleton;
            }
        }
    }
}





