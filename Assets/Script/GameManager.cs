using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject door;
    public float rotateSpeed = 20f;
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    public GameObject playerPrefab; // 생성할 플레이어 캐릭터 프리팹

    //private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태

    // 주기적으로 자동 실행되는, 동기화 메서드


    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // 게임 시작과 동시에 플레이어가 될 게임 오브젝트를 생성
    private void Start()
    {
        // 생성할 랜덤 위치 지정
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        // 위치 y값은 0으로 변경
        randomSpawnPos.y = 0f;
        door = GameObject.Find("piv");
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    //// 점수를 추가하고 UI 갱신
    //public void AddScore(int newScore)
    //{
    //    // 게임 오버가 아닌 상태에서만 점수 증가 가능
    //    if (!isGameover)
    //    {
    //        // 점수 추가
    //        score += newScore;
    //        // 점수 UI 텍스트 갱신
    //        UIManager.instance.UpdateScoreText(score);
    //    }
    //}
    public void DoorOpen()
    {
        if(!isGameover)
        {
            door.transform.LeanRotateY(120f, 0.1f);
            //door.transform.LeanRotateY(120f, 0.2f);

            //GameObject.FindWithTag("Door").LeanRotate(new Vector3(0, 90, 0), 10f);
        }
        
    }

    public void DoorClose()
    {
        if(!isGameover)
        {
            door.transform.LeanRotateY(0f, 0.2f);
        }
    }

    // 게임 오버 처리
    public void EndGame()
    {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);
    }
}
