using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    int QuantityOfEnemies = 5; //esta variavel foi criada para spawnar 5 inimigos quando o personagem pegar uma quantidade x de moedas

    void Start()// aqui em baixo só acontece uma vez no começo do jogo, logo quando aperta start   
    {
        foreach (GameObject spawnpoitnpai in GameObject.FindGameObjectsWithTag("spawnpointspai"))
        {               
                                                                                                     //aqui o iterador é o spawnpoitnpai?sim
                                                                                                    //neste foreach o spawnpoitnpai ganha a localização de todos os pais?sim  

                                                                                                      //dentro do foreach o FindGameObjectsWithTag é uma função do gameobject q retorna um array com todos os objetos com essa tag 
                                                                                                      //**aqui o GameObject.FindGameObjectsWithTag("spawnpointspai")) ao ser chamado em qualquer lugar sempre retorna um array com todos os objetos com esta tag
                                                                                  

            List<Transform> listspawns = new List<Transform>();                                       

            foreach (Transform filhos in spawnpoitnpai.transform)  //aqui o foreach ja sabe aumaticamente q cada pai tera no maximo 5 filhos, por ja ter definido isto na hierarchy? sim
            {                                    
                                                                   //como aqui ja se sabe a localização dos pais, ja se sabe tambem a dos filhos?sim
                                                                  // este foreach diz q para cada localização de filho em cada spawnpoitnpai q tem a localização dos pais sera adicionado 1 na listspawns?sim   

                                                                    //**aqui a variavel.transform é uma função q retorna o array com todos os filhos(mas só pq é uma exceção do foreach)

                listspawns.Add(filhos);  
            }                            

            listDosSpawnPointsCasas.Add(listspawns);  
        }

        SpawnarMoedasNoNivel(0, CoinPrefabAzul);   
    }                                            //o zero em parenteses se refere ao nivel zero?sim
    
    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnarMoedasNoNivel(int n, GameObject moedaPrefab)//spawnar e criar é o mesmo
    {
        // caso o nivel passado seja uma posição que não existe na lista || (ou) o prefab passado não contém o componente Coin
        if (n >= listDosSpawnPointsCasas.Count || n < 0 || moedaPrefab.GetComponent<Coin>() == false) return; //aqui o Getcomponent esta checando se a moeda prefab tem o componente coin
                                                                                                              // o listDosSpawnPointsCasas.Count é a quantidade de niveis que a fase tem
        for (int i = 0; i < listDosSpawnPointsCasas[n].Count; i++) //sempre q um array ou lista estiver com um indice dentro do colchetes[i] esta falando do elemento nesta possição q é o indice e não a lista em si
        {
            GameObject go = Instantiate(moedaPrefab, listDosSpawnPointsCasas[n][i].position, Quaternion.identity);//o Quaternion.identity é para o objeto manter a rotação q começou

            listcoins.Add(go.GetComponent<Coin>());
        }

    }


}
