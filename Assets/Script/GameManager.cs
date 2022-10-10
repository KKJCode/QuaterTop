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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; // �̱����� �Ҵ�� static ����

    public GameObject playerPrefab; // ������ �÷��̾� ĳ���� ������

    //private int score = 0; // ���� ���� ����
    public bool isGameover { get; private set; } // ���� ���� ����

    // �ֱ������� �ڵ� ����Ǵ�, ����ȭ �޼���


    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
    }

    // ���� ���۰� ���ÿ� �÷��̾ �� ���� ������Ʈ�� ����
    private void Start()
    {
        // ������ ���� ��ġ ����
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        // ��ġ y���� 0���� ����
        randomSpawnPos.y = 0f;
        door = GameObject.Find("piv");
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    //// ������ �߰��ϰ� UI ����
    //public void AddScore(int newScore)
    //{
    //    // ���� ������ �ƴ� ���¿����� ���� ���� ����
    //    if (!isGameover)
    //    {
    //        // ���� �߰�
    //        score += newScore;
    //        // ���� UI �ؽ�Ʈ ����
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

    // ���� ���� ó��
    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameover = true;
        // ���� ���� UI�� Ȱ��ȭ
        UIManager.instance.SetActiveGameoverUI(true);
    }
}
