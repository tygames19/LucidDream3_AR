

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlocking : MonoBehaviour
{
    //flocking behaviour script
    CreateFish ins_fish;
    public GameObject Benchmark_;
    public float speed = 0.001f;
    float rotation_speed = 3.0f;
    float neighbour_distance_ = 3.0f;

    Vector3 averageHeading;
    Vector3 averagePosition;

    Vector3 rot_direction;

    bool turning = false;
    //범위의 끝에 닿을경우

    // Use this for initialization
    void Start()
    {
        ins_fish = FindObjectOfType<CreateFish>();
        Benchmark_ = ins_fish.Benchmark;
        speed = Random.Range(0.1f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Benchmark_.transform.position) >= 3)
        {//######## 여기에 숫자10이 전체의 범위입니다. 조정하셔야 하는 부분입니다. ########
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning == true)
        {

            Vector3 direction = Benchmark_.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                rotation_speed * Time.deltaTime);

            speed = Random.Range(0.1f, 0.3f);
        }
        else if (Random.Range(0, 5) < 1)
        {
            Applyrules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void Applyrules()
    {
        GameObject[] allFish;
        allFish = CreateFish.FishClone;//모든 생성된 물고기를 불러옴

        Vector3 center_ = Vector3.zero;//그룹의 센터
        Vector3 avoid_ = Vector3.zero;//피함의 벡터(어느 가까운 물고기와 피하는 정도)
        Vector3 goal = CreateFish.goalPos;
        float group_speed = 0.05f;
        float dist;
        int groupSize = 0;


        foreach (GameObject eachFish in allFish)
        {
            if (eachFish != gameObject)
            {
                dist = Vector3.Distance(eachFish.transform.position, gameObject.transform.position);
                if (dist <= neighbour_distance_)//이웃과 2.0이상 가까웠을때
                {//그럼 너는 그룹이다
                    center_ += eachFish.transform.position;
                    //그룹의 센터의 평균을 내기위해 그룹에 들어온 물고기의 포지션값을 추가
                    groupSize++;
                    //그룹의 크기도 키워줌(계산용)
                    if (dist < 1.0f)
                    {//너무 가까우면
                        avoid_ = avoid_ + (gameObject.transform.position - eachFish.transform.position);
                    }
                    FishFlocking ins_flock = eachFish.GetComponent<FishFlocking>();
                    group_speed = group_speed + ins_flock.speed;
                    //그룹의 평균 속도를 찾아서(그룹에 있는 모든 물고기의 스피드를 더함으로서)
                }
            }
        }

        if (groupSize > 0)//물고기가 그룹안에 들어있다면
        {
            center_ = center_ / groupSize + (goal - gameObject.transform.position);
            //그룹의 평균센터를 계산
            speed = group_speed / groupSize;
            //그룹의 평균속도를 계산

            Vector3 direction = (center_ + avoid_) - transform.position;//방향을 계산
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotation_speed * Time.deltaTime);
            }
        }
    }
}

