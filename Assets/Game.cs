using System.Collections;
using System.Collections.Generic; // algumas funcções adicionais
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Game : MonoBehaviour {                    //para deixar um prefab com seu script, sem precisar ficar arrastando sempre o script para o gameobject na hierarchy, eu posso simplesmente arrastar o script uma vez para o objeto na hierarchy e depois arrasatar o objeto da hierarchy para os assets, assim ele ficaria direto sempre com os scripts? isso seria criar um prefab?   


    //parte diferente
    public GameObject coinPrefabAmarela;//aqui é para criar pelo codigo
    public GameObject coinPrefabAzul;//aqui é para criar pelo codigo


    public GameObject coinPrefab;//aqui é para criar pelo codigo


    public GameObject inimigoPrefab;

    public List<Transform> spawnPointsMoedas = new List<Transform>();

    public List<Coin> listCoins = new List<Coin>();
    public List<Transform> spawnPointsInimigos = new List<Transform>();
    public List<Inimigo> listInimigos = new List<Inimigo>();

    public List<List<Transform>> listasSpawnPointsCasas = new List<List<Transform>>();//pq esta lista não aparece no inspector? ja q ela contem todas as listas spawnlists? 

    public List<List<Transform>> listasDosInimigosPais = new List<List<Transform>>();
    //quantidade de inimigos para spawnar
    [SerializeField]
    public int quantityOfEnemies = 5;

    [SerializeField]
    List<GameObject> pais;





    // Use this for initialization
    void Start() { //no vois start só se vai as informações q são realizdas apenas uma vez no começo do jogo?

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

        //Traversamos por todos os objetos com a tag SpawnPointPai                                     //para q neste caso procurar pela tag "SpawnPointsPai"?                              
        foreach (GameObject spawnPoitnPai in GameObject.FindGameObjectsWithTag("SpawnPointsPai"))    //o spawnPoitnPai foi criado aqui para guardar um valor? , e é do tipo Gameobject? 
        {                                                                                         //o foreach é para cada elemento(neste caso as moedas) executar uma operação?
                                                                                                  //para q procurar pela tag "SpawnPointsPai"?
                                                                                                  //o spawnPoitnPai é variavel para guardar...?
                                                                                                  // o que é travesar?
                                                                                                  //para q usar o in em vez do =?
                                                                                                  //o foreach é para cada elemento filho executar uma operação, q seria ser destruido durante a colisão com o player?  


            List<Transform> listSpawns = new List<Transform>();// este é para guardar os 5 spawn points filhos

            //traversar os filhos dos pais
            foreach (Transform filhos in spawnPoitnPai.transform)  //os filhos foram criados aqui e são do tipo transform por que quer pegar só a localização dos filhos? 
            {
                listSpawns.Add(filhos); //aqui acha um filho e ja o coloca na listSpawns?   

            }


            //adicionamos a lista criada para a nossa Lista de listas
            listasSpawnPointsCasas.Add(listSpawns);         //aqui apos adicionar um numero variavel de filhos em uma listSpawns, é adicionado uma listspawns na listasSpawnPointsCasas? 

        }

        //moedas do nivel 0
        SpawnarMoedasNoNivel(0, coinPrefabAzul);



        pais = GameObject.FindGameObjectsWithTag("SpawnPointPaiinimigos").OrderBy(go => go.name).ToList();

        foreach (GameObject spawnpoitnsinimigos in pais)
        {
            List<Transform> listSpawnInimigos = new List<Transform>();//esta lista fica com os 100 filhos e sem os pais 

            foreach (Transform inimigosfilhos in spawnpoitnsinimigos.transform)//aqui monta um array só com os filhos? não os pais?
            {

                listSpawnInimigos.Add(inimigosfilhos);
            }
            listasDosInimigosPais.Add(listSpawnInimigos);//a lista de listas ja vai estar com os filhos divididos
        }

        

        SpawnarInimigosNoNivel(0, inimigoPrefab);//pq este comando é colocado aqui logo apos colocar informação na lista de listas?




    }

    // Update is called once per frame
    void Update() {

        

        










    }

    public void SpawnarMoedasNoNivel(int n, GameObject moedaPrefab)
    {
        // caso o nivel passado seja uma posição que não existe na lista || (ou) o prefab passado não contém o componente Coin
        if (n > listasSpawnPointsCasas.Count || moedaPrefab.GetComponent<Coin>() == false) return; //aqui o Getcomponent esta checando se a moeda prefab tem o componente coin

        for (int i = 0; i < listasSpawnPointsCasas[n].Count; i++)
        {
            GameObject go = Instantiate(moedaPrefab, listasSpawnPointsCasas[n][i].position, Quaternion.identity);//o Quaternion.identity é para o objeto manter a rotação q começou

            listCoins.Add(go.GetComponent<Coin>()); //aqui esta verificando se realmente se trata de uma moeda?
        }

        //spawnpointsmoedas.Add(go)

    }

    public void RemoveCoinFromList(GameObject coin)
    {
        if (coin.GetComponent<Coin>())// aqui serve para checar se o objeto coin tem o script coin
        {
            listCoins.Remove(coin.GetComponent<Coin>());
        }
    }

    void spawnEnemy(Vector3 position)
    {
        GameObject go = Instantiate(inimigoPrefab, position, Quaternion.identity);

        listInimigos.Add(go.GetComponent<Inimigo>());

    }
     public void SpawnarInimigosNoNivel(int n, GameObject inimigoPrefab)   //os GameObjects q spawnam tem q ter apenas o componente transform? sim
    {
        if (n != 0)
            print("tamanho da lista dos pais " + listasDosInimigosPais.Count);

        if (n >= listasDosInimigosPais.Count || n < 0 || inimigoPrefab.GetComponent<Inimigo>() == false) return; //por que o tamanho dela é 10 mas vai até o 9, o n vai até o 9

       // print("tttttttttttt ");

        for (int i=0;i<listasDosInimigosPais[n].Count;i++) //o listasDosInimigosPais[n] representa o pai daquela possição e o count os seus filhos
        {
            GameObject ru = Instantiate(inimigoPrefab, listasDosInimigosPais[n][i].position, Quaternion.identity);

            ru.transform.parent = pais[n].transform;

            listInimigos.Add(ru.GetComponent<Inimigo>()); 
        }



    }
    public void RemoverInimigoDaLista(GameObject inimigo) //sempre depois de um GameObject ser spawnado ele deve ser removido da lista para não voltar mais?
    {
        if(inimigo.GetComponent<Inimigo>())
        {
            listInimigos.Remove(inimigo.GetComponent<Inimigo>());
            Destroy(inimigo);
        }
    }

   


}  
