using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateFish : MonoBehaviour
{
    public GameObject fishPrefab;
  //  public GameObject specialPoint;

    public GameObject Benchmark;
    /*######## 중심점으로 들어갈 오브젝트입니다. 퍼블릭으로 넣어주세요.########*/

    public static Vector3 goalPos;// = mainCamera_.transform.position;
    public static GameObject[] FishClone = new GameObject[20];
    /*######## 생성될 물고기 개수입니다. 열대어 1종류당 30개씩 총 90개가 생성됩니다.
            조정하셔야 하는 부분입니다.########*/

    public GameObject FishManager;//물고기 부모로 씬 관리하기 위해서 생성

    int range_ = 10;
    Vector3 randomRange;
    // Use this for initialization
    void Start()
    {
        goalPos = Benchmark.transform.position;
        for (int i = 0; i < 20; i++)
        {/*######## 생성될 물고기 개수입니다. 열대어 1종류당 30개씩 총 90개가 생성됩니다.
           조정하셔야 하는 부분입니다.########*/

            Vector3 camMarked_pos = Benchmark.transform.position;
            Vector3 for_random_minus = camMarked_pos - new Vector3(range_, 0, range_);
            Vector3 for_random_plus = camMarked_pos + new Vector3(range_, 4.5f, range_);
            print(for_random_minus + " // " + for_random_plus);
            randomRange = new Vector3(
                Random.Range(for_random_minus.x, for_random_plus.x),
                Random.Range(for_random_minus.y, for_random_plus.y),
                Random.Range(for_random_minus.z, for_random_plus.z));

            FishClone[i] = Instantiate(fishPrefab, randomRange, Quaternion.identity);
            float ranSize = Random.Range(1f, 1.8f);
            FishClone[i].transform.localScale = new Vector3(ranSize, ranSize, ranSize);
            FishClone[i].transform.parent = FishManager.transform;
            /*
            if (i == 10)
            {
                GameObject special = Instantiate(specialPoint, randomRange, Quaternion.identity);
                special.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                //special.transform.SetParent(FishClone[i].transform, false);
            }*/
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Random.Range(0, 10000) < 50)
        {
            Vector3 camMarked_pos = Benchmark.transform.position;
            Vector3 for_random_minus = camMarked_pos - new Vector3(2.5f, 1.5f, 2.5f);
            Vector3 for_random_plus = camMarked_pos + new Vector3(2.5f, 1.5f, 2.5f);

            goalPos = new Vector3(
                Random.Range(for_random_minus.x, for_random_plus.x),
                Random.Range(for_random_minus.y, for_random_plus.y),
                Random.Range(for_random_minus.z, for_random_plus.z));//new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        }
    }
}