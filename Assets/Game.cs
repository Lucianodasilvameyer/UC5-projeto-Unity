using System.Collections;
using System.Collections.Generic;
using System.Linq; // algumas funcções adicionais
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {


    public GameObject coinPrefab;//aqui é para criar pelo codigo

    public GameObject inimigoPrefab;                             

    public List<Transform> spawnPoints = new List<Transform>();

    public List<Coin> listCoins = new List<Coin>();
    public List<Inimigo> listInimigos = new List<Inimigo>();

    //quantidade de inimigos para spawnar
    [SerializeField]
    int quantityOfEnemies = 5;

  public  MeshRenderer meshDoPlano;

    
    // Use this for initialization
    void Start () {

        //meshDoPlano = GameObject.FindGameObjectWithTag("Ground").GetComponent<MeshRenderer>(); 

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject go = Instantiate(coinPrefab, new Vector3(i * 1.0f, 2, j * -1.0f), Quaternion.identity);//o Quaternion.identity é para o objeto manter a rotação q começou

                listCoins.Add(go.GetComponent<Coin>());
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
        if(listCoins.Count == 80)
        {
            print("POUCAS MOEDAS");

            for(int i =0; i < quantityOfEnemies; i++)
            {

                if (spawnPoints.Any()) // caso tenha algum elemento na lista
                {
                    int pos = Random.Range(0, spawnPoints.Count - 1);
                    spawnEnemy(spawnPoints[pos].position);

                    spawnPoints.RemoveAt(pos);
                }
                else break;
                
            }
        }

        if(listCoins.Count <= 0)
        {
            SceneManager.LoadScene(1);
        }
       
	}

    public void RemoveCoinFromList(GameObject coin)                     
    {
        if(coin.GetComponent<Coin>())// aqui serve para checar se o objeto coin tem o script coin
        {
            listCoins.Remove(coin.GetComponent<Coin>());
        }
    }

    void spawnEnemy(Vector3 position)
    {
        GameObject go = Instantiate(inimigoPrefab, position, Quaternion.identity);

        listInimigos.Add(go.GetComponent<Inimigo>());

    }
}
