using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {


    public Transform target; // alvo para o inimigo seguir

    public float speed = 5f;
    public int hp = 2;

    [SerializeField]
    private float timerMax; //tempo maximo do timer
    public float timer = 0; // timer em si
    bool goLeft = true;

    

    [SerializeField]
    GameObject bullet;

    public Game game_ref;

    // Use this for initialization
    void Start () {

        timer = Time.time;
        //pegamos a referencia do jogador
        target = GameObject.FindGameObjectWithTag("Player").transform;
        game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();//se o GetComponent não estiver dentro do if e estiver depois de um sinal de igual ele esta salvando(colocando a classe) em uma variavel







    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(target);
        //followTarget();

        if (Time.time >= timer + timerMax)
        {
            goLeft = !goLeft;
            timer = Time.time;
            shootAt(target);
        }

        //moveSideways(goLeft);

        
       
    }


    void followTarget()
    {
        Vector3 input = target.position - transform.position;
        //normalizamos para pegar apenas a direção



        Vector3 direction = input.normalized;
        //nossa velocidade será a direção multiplicada pela nossa velocidade
        Vector3 velocity = direction * speed;



        //por fim a distância para percorrer será essa velocidade multiplicada pelo tempo
        Vector3 moveAmount = velocity * Time.deltaTime;

        //aqui iremos mover nosso jogador pela distância que iremos percorrer
        transform.Translate(moveAmount);

    }

    void moveSideways(bool goLeft)
    {
        Vector3 direction;
        if (goLeft) direction = Vector3.left;
        else direction = Vector3.right;

        // Vector3 direction = (goLeft) ? Vector3.left : Vector3.right;

        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        transform.Translate(moveAmount);

       

    }

    public void TomarDano(int dano)
    {
        hp -= dano;

        print("Tomou dano");
        if(hp <= 0)
        {
            game_ref.RemoverInimigoDaLista(gameObject);

        }
    }


    //função para atirar num alvo
    void shootAt(Transform target)
    {
        Vector3 instantiatePosition = transform.position + transform.forward * 1.5f;
        //instantiatePosition.y = target.position.y;

        GameObject go =  Instantiate(bullet, instantiatePosition, Quaternion.identity);
        //go.transform.LookAt(target);
        
       

    }
}
