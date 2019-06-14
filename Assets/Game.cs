using System.Collections;
using System.Collections.Generic;
using System.Linq; // algumas funcções adicionais
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    //parte diferente
    public GameObject coinPrefabAmarela;//aqui é para criar pelo codigo
    public GameObject coinPrefabAzul;//aqui é para criar pelo codigo

    public GameObject inimigoPrefab;                             

    public List<Transform> spawnPoints = new List<Transform>();

    public List<Coin> listCoins = new List<Coin>();
    public List<Inimigo> listInimigos = new List<Inimigo>();

    public List<List<Transform>> listasSpawnPointsCasas = new List<List<Transform>>();

    //quantidade de inimigos para spawnar
    [SerializeField]
    int quantityOfEnemies = 5;

  

    
    // Use this for initialization
    void Start () {

        //meshDoPlano = GameObject.FindGameObjectWithTag("Ground").GetComponent<MeshRenderer>(); 

        /*
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject go = Instantiate(coinPrefabAmarela, new Vector3(i * 1.0f, 2, j * -1.0f), Quaternion.identity);//o Quaternion.identity é para o objeto manter a rotação q começou

                listCoins.Add(go.GetComponent<Coin>());
            }
        }

        */

        //Traversamos por todos os objetos com a tag SpawnPointPai
        foreach( GameObject spawnPoitnPai in GameObject.FindGameObjectsWithTag("SpawnPointsPai"))
        {
            List<Transform> listSpawns = new List<Transform>();// este para guardar os 5 spawn points 

            //traversar os filhos dos pais
            foreach(Transform filhos in spawnPoitnPai.transform)  //o transform é por que quer pegar só a localização dos filhos
            {
                listSpawns.Add(filhos);

            }

           
            //adicionamos a lista criada para a nossa Lista de listas
            listasSpawnPointsCasas.Add(listSpawns);

        }


        
       

        //moedas do nivel 0
        SpawnarMoedasNoNivel(0, coinPrefabAzul);


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

        if(false)
        {
            SceneManager.LoadScene(1);
        }
       
	}

    public void SpawnarMoedasNoNivel(int n, GameObject moedaPrefab)
    {
        // caso o nivel passado seja uma posição que não existe na lista || (ou) o prefab passado não contém o componente Coin
        if (n > listasSpawnPointsCasas.Count || moedaPrefab.GetComponent<Coin>() == false) return; //aqui o Getcomponent esta checando se a moeda prefab tem o componente coin

        for (int i = 0; i < listasSpawnPointsCasas[n].Count; i++)
        {
            GameObject go = Instantiate(moedaPrefab, listasSpawnPointsCasas[n][i].position, Quaternion.identity);//o Quaternion.identity é para o objeto manter a rotação q começou

            listCoins.Add(go.GetComponent<Coin>());
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
