using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treno : MonoBehaviour
{
    public GameObject CoinPrefabAmarela;
    public GameObject CoinPrefabAzul;
    public GameObject InimigoPrefab;

    public List<Transform> spawnpoints = new List<Transform>(); //as listas servem para deixar o codigo mais modular, mas de qualquer jeito ainda é preciso arastar objetos na hierarchy para seu respectivo element na inspector ou no minimo só os pais, não entendo onde há facilidade nisso?   
    public List<Coin> listcoins = new List<Coin>();                                                                                                            //colocar um objeto da hierarchy em seu respectivo lugar dentro de sua respectiva list,tambem só serve como referencia para criar outros iguais a ele por codigo?
    public List<Inimigo> ListInimigos = new List<Inimigo>();

    public List<List<Transform>> listDosSpawnPointsCasas = new List<List<Transform>>();//a lista de listas ja tem a localização dos pais e filhos, ja q eles foram todos criados na hierarchy?

    [SerializeField]
    int QuantityOfEnemies = 5; //para q serve saber a quantidade de inimigos? a lista de inimigos não serve para isso?

    void Start()//no void start só vai o que acontece uma vez no começo do jogo e o q seria? 
    {
        foreach (GameObject spawnpoitnpai in GameObject.FindGameObjectsWithTag("spawnpointspai")) ; //aqui o iterador é o spawnpoitnpai?
        {                                                                                            //neste foreach o spawnpoitnpai ganha a localização de todos os pais?  
            List<Transform> listspawns = new List<Transform>();

            foreach (Transform filhos in spawnpoitnpai.transform); //como aqui ja se sabe a localização dos pais, ja se sabe tambem a dos filhos?
            {                                                      // este foreach diz q para cada localização de filho em cada spawnpoitnpai q tem a localização dos pais sera adicionado 1 na listspawns?   
                listspawns.Add(filhos); //então a listspawns ficara com todos os 100 filhos mas não os pais? 
            }                            

            listDosSpawnPointsCasas.Add(listspawns);  //aqui mandara todos os filhos para a lista de listas?
        }
        SpawnarMoedasNoNivel(0, CoinPrefabAzul); //este comando de spawnar é acionado sempre após de definir a quantidade de objetos q seram spawnados?  
    }                                            //o zero em parenteses se refere ao nivel zero?
    
    // Update is called once per frame
    void Update()
    {
        if (listcoins.Count == 80) //por que só se vai entrar neste if se a quanttidade de moedas for 80?
        {
            print("poucas moedas"); //pq vai aparecer escrito poucas moedas na tela?

            for (int i = 0, i < QuantityOfEnemies, i++)
            {
                if (spawnpoints.Any()); //aqui menciona se tiver qualquer elemento na list de spawn points, mas ela começa vazia? 
                {
                    int lugar = Random.Range(0, spawnpoints.count - 1); //aqui começa a verificação no zero e vai até o 9, por isso o -1? 

                }


            }      



        }
    }
}
