using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    float speed;
    int hp;
    float heightCoeff;
    bool inRun;
    bool deadMan;
    public bool iCanShot;
    public GameObject car;
    public GameObject blood;
    public GameObject enemyBullet;
    GameObject enemyScope;
    [SerializeField] int numberOfshoots = 5;
    public int health;
    Vector3 enemyTarget;
    //public GameObject carRendered;
    [SerializeField] float runEnemyTime;
    [SerializeField] float dieTime;
    GameObject shotPoint;
    GameObject shotPoint1;
    LineRenderer lineRenderer;
    public enum EnemyState { run, shot, idle, die };
    public EnemyState enemyState;

    void Start()
    {
        speed = 0.05f;
        anim = GetComponent<Animator>();
        enemyScope = transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        shotPoint = transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        car = GameObject.Find("carRendered");
        CheckBehaivor();
        enemyScope.SetActive(false);
        lineRenderer = GetComponent<LineRenderer>();
        enemyState = EnemyState.run;
        blood.SetActive(false);
        Invoke("EnemyDieByHelicopter", dieTime);

        //Destroy(gameObject,1f);
    }

    void EnemyDieByHelicopter()
    {
        if (enemyState != EnemyState.die)
        {
            EnemyDie();
            blood.SetActive(true);
        }
    }

    void CheckBehaivor()
    {
        if (gameObject.tag == "EnemyBuilding")
        {
            if (transform.position.y < 1)
            {
                heightCoeff = Random.Range(0.2f, 0.8f);
            }

            else
            {
                heightCoeff = 0.25f;
            }


            if (transform.parent.parent.parent.name == "CityDot1")
            {
                enemyTarget = transform.position + new Vector3(35f * heightCoeff, 0f, 0f);

            }

            else if (transform.parent.parent.parent.name == "CityDot")
            {
                enemyTarget = transform.position + new Vector3(-35f * heightCoeff, 0f, 0f);

            }

            else if (transform.parent.parent.parent.name == "CityDot0rot")
            {
                enemyTarget = transform.position + new Vector3(0, 0f, 35f * heightCoeff);
                gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            }

            else if (transform.parent.parent.parent.name == "CityDot1rot")
            {
                enemyTarget = transform.position + new Vector3(0f, 0f, -35f * heightCoeff);
                gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            }
            anim.SetInteger("enemyBehaivour", 0);
            Invoke("RunEnemy", runEnemyTime);
        }
        //carRendered = GameObject.Find("carRenderer");
        else if (gameObject.tag == "EnemyCar")
        {
            transform.LookAt(car.transform.position);
            anim.SetInteger("enemyBehaivour", 0);
            Invoke("EnemyShot", 1f);
            //Invoke("ChangeAnimShot",1f);
        }

    }


    public void RunEnemy()
    {
        //anim.SetInteger("enemyBehaivour", 0);
        enemyState = EnemyState.run;
        //anim.Play("handgun_combat_run");
        inRun = true;
        transform.LookAt(enemyTarget);

    }

    //void ChangeAnimShot()
    //{
    //    anim.SetInteger("enemyBehaivour", 1);
    //}

    void EnemyShot()
    {
        inRun = false;
        enemyState = EnemyState.shot;
        anim.SetInteger("enemyBehaivour", 3);
        enemyScope.SetActive(true);
        GameObject enemyBulletFly = Instantiate(enemyBullet, enemyScope.transform.position, transform.rotation);
        Invoke("EnemyScopeOff", 0.3f);
        numberOfshoots--;
    }

    void EnemyScopeOff()
    {
        enemyScope.SetActive(false);
        anim.SetInteger("enemyBehaivour", 2);
        EnemyIdle();
    }

    void EnemyIdle()
    {
        enemyState = EnemyState.idle;
        if (numberOfshoots > 0)
        {
            Invoke("EnemyShot", 1f);
        }

        //else
        //{
        //    Invoke("EnemyDie", 10f);
        //}
        enemyScope.SetActive(false);
    }
    public void EnemyDie()
    {
        if (!deadMan)
        {
            deadMan = true;
            int randDie = Random.Range(20, 23);
            Invoke("BloodOut", 0.5f);
            switch (randDie)
            {
                case 20:
                    anim.SetInteger("enemyBehaivour", 20);
                    anim.Play("handgun_death_A");
                    break;
                case 21:
                    anim.SetInteger("enemyBehaivour", 21);
                    anim.Play("handgun_death_B");
                    break;
                case 22:
                    anim.SetInteger("enemyBehaivour", 22);
                    anim.Play("handgun_death_C");
                    break;
            }

            Destroy(gameObject, 2f);
        }
    }

    private void BloodOut()
    {
        //blood.SetActive(false);
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, enemyTarget);
        //transform.LookAt(car.transform.position);
        if (enemyState == EnemyState.run)
        {
            transform.Translate(Vector3.forward * speed);
        }

        if (dist <= 0.2f && !iCanShot)
        {
            iCanShot = true;
            anim.SetInteger("enemyBehaivour", 4);
            EnemyIdle();
        }

        if (enemyState == EnemyState.shot || enemyState == EnemyState.idle)
        {
            transform.LookAt(car.transform.position);
            //lineRenderer.SetPosition(0, shotPoint.transform.position);
            //lineRenderer.SetPosition(1, car.transform.position);
        }
    }
}
