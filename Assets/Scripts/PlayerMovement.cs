using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlayer;
    public float speed;
    private Animator PlayerAnim;

    public GameObject target;
    public GameObject target1;

    [SerializeField]private GameObject Confetti;
    
    public bool Flag1;
    public bool Flag2;
    public bool CanAttack = false;
    [SerializeField]private GameObject WinScreen;
    [SerializeField]private GameObject GameOverScreen;
       
 // Start is called before the first frame update
    private void Awake()
    {
        PlayerAnim = GetComponent<Animator>();
        Flag1 = true;
        Flag2 = false;
    }

        // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if(target != null)
        {
            if ((Vector3.Distance(target.transform.position, transform.position)) > 5f && Flag1)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                PlayerAnim.SetBool("Run",true);
            }
            if ((Vector3.Distance(target.transform.position, transform.position)) <= 5f && Flag1)
            {
                transform.position = this.transform.position;
                PlayerAnim.SetBool("Run",false);
                Flag1 = false;
                CanAttack = true;
            }
        }else{return;}
        

        if ((Vector3.Distance(target1.transform.position, transform.position)) > 1f && Flag2)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, step);
            transform.LookAt(target1.transform.position);
            PlayerAnim.SetBool("Run",true);
        }


        if ((Vector3.Distance(target1.transform.position, transform.position)) <= 1f && Flag2)
        {
            transform.position = this.transform.position;
            Flag2 = false;
            PlayerAnim.SetTrigger("Win");
            if(isPlayer)
            {
                WinScreen.SetActive(true);
                Confetti.SetActive(true);
            }
            else
            {
                GameOverScreen.SetActive(true);
            }
            
        }

        if(CanAttack){PlayerAnim.SetTrigger("Punch");}
    }
    
    private void Destoy(GameObject enemy)
    {
        enemy = target.gameObject;
        enemy.GetComponent<Animator>().SetTrigger("Dead");
        Destroy(enemy,2.3f);
        //enemy.SetActive(false);
        Flag2 = true;
    }
}

