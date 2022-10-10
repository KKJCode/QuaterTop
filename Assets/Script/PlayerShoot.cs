using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerShoot : MonoBehaviour
{
    public Gun gun;
    //public Transform gunPivot; //�ѹ�ġ ������
    //public Transform lefthand;  // �ѿ��ʼ����� �޼���ġ ����
    //public Transform righthand; // �ѿ����� ������ ������ ��ġ ����
    private PlayerInput playerInput; // �÷��̾��Է�
    private Animator playerAnim; // �ִϸ����� ������Ʈ



    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>(); 
    }
    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1")/*playerInput.fire*/)
        {
            gun.Fire();
        }
        else if(CrossPlatformInputManager.GetButtonDown("Reload"))
        {
            if (gun.Reload())
            {
                //playerAnim.SetTrigger("Reload");
            }
        }
        else if(CrossPlatformInputManager.GetButtonDown("GRB1"))
        {
            Debug.Log("Check");
            gun.Granade();
        }
        

        UpdateUI();
    }

    

    void UpdateUI()
    {
        if(gun != null && UIManager.instance != null)
        {
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }
}
