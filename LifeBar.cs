using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] float maxHP = 10f;
    [SerializeField] GameObject Gamemanager;
    float currentHp;
    float damage = 0.01f;
    GameManager gameManager;

    private void Start()
    {
        currentHp = maxHP;
        gameManager = Gamemanager.GetComponent<GameManager>();
    }
    public void MinusLife()
    {
        currentHp -= damage;

        if (currentHp < 0)
        {
            gameObject.transform.localScale = Vector3.zero;
            GameOver();

        }
        else
        {
            gameObject.transform.localScale -= new Vector3(damage, 0f, 0f);
        }
    }

    void GameOver()
    {
        gameManager.LevelEnd();
    }
}
