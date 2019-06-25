﻿using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // algumas funcções adicionais
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    //parte diferente
    public float speed = 2f;
    public float jumpHeight = 5f;
    public int coinCountAmarelo = 0;
    public int coinCountAzul=0;
    public int nivelCasa = 0;

    public int hp = 10;

    public float walkSpeed = 3f;
    public float runSpeed = 8f;
    public float gravity = -12f;

    public float timer;
    public float timerMax = 5;

    [SerializeField]
    float velocityY;

    [SerializeField]
    Sword sword;

    [SerializeField]
    bool running = false;

    [SerializeField]
    bool atacando = false;
    [SerializeField]
    bool isOnCooldown = false;



    [SerializeField]
    Game game_ref;
                               //**para fazer referencias**
    public MeshRenderer mesh; //ao criar uma variavel meshrenderer no script do player, aparecera no inspector o seu nome no caso "mesh", dai é só arrastar o object da hierarchy o qual quer usar o seu "meshrenderer" para o script na parte escrita "mesh"?sim.o mesh renderer é tanto a classe do mesh quanto a classe q se quer usar do plane?sim


    public TextMeshProUGUI textoamarelo; // aqui todas as variaveis aparecem no inspector
    public TextMeshProUGUI textoazul;
    public TextMeshProUGUI textoHP;
    public TextMeshProUGUI textoGameover;

    float smoothRotationVelocity;
    [SerializeField]
    float smoothRotationTime = 0.2f;

    float smoothSpeedVelocity;
    [SerializeField]
    float smoothSpeedTime = 0.2f;

    [SerializeField]
    Transform cameraT;

    [SerializeField]
    CharacterController charController;

    [SerializeField]
    Animator animator;

    // Use this for initialization
    private void Start()
    {
        //pegando referências
        cameraT = Camera.main.transform;

       // Rigidbody cameraGO = Camera.main.transform.GetComponent<Rigidbody>();
        //cameraGO.AddForce(Vector3.up * 10);
                                                                                //só não é necessario fazer referencia de script?
        charController = GetComponent<CharacterController>(); //deve-se pegar a referencia do characterController para o player poder usar esse component

        animator = GetComponent<Animator>();//deve-se pegar a referencia do animator para o player poder usar esse component


        // speed = 2f;
        
        textoamarelo.text = "Coins: " + coinCountAmarelo;
        textoazul.text = "Coins: "+coinCountAzul; //o + é para o numero aparecer
        textoHP.text = "HP: " + hp;

       // timer = Time.time; //esse é o q esta sendo usado para marcar o cooldown de ataque

        //Instantiate(coinPrefab, new Vector3(2, 2, 2), Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isOnCooldown == false)
        {
            
                                         
                timer = Time.time;    
                                       
            sword.timer = Time.time;   
            sword.gameObject.SetActive(true);
                

                isOnCooldown = true;


                                            //o timer esta sendo usado para guardar o instante de agora(o tempo de quando se apertou)(exemplo se eu usar a espada no instante 6 esse tempo sera salvo e só vai ser usada a partir do 11) //aqui é o timer do cooldown               

                                       //o Time.time é o tempo corrido q só vai aumentando
                                                  //o sword.timer é usado para guardar o valor do momento q usou a espada para saber quando apagar ela 
            
     

        }

        
                                                                      //assim como esta em baixo sera usado menos recursos computacionais e tera menos possibilidades do jogo travar
        if (Time.time >= timer + timerMax && isOnCooldown == true)// aqui checa se já passou timerMax(5) segundos
        {                                   //No Time.time  é o tempo da unity q representa sempre quanto tem po passou desde o inicio do jogo   
                                            //o timer é o nosso tempo de jogo q serve para saber quando se atacou e esse tempo fica salvo
            
            isOnCooldown = false;//o cooldown só pode ser utilizado quando esta false
            
        }

        walkingRotating();
        //walkSideways();

       


    }

    //logica para andar rotacionando
    void walkingRotating()
    {
        // pegar input do jogador
        // Input.getaxis retorna um valor de -1 a 1
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //normalizamos para pegar apenas a direção
        Vector2 inputDir = input.normalized;



        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        //rotação
        if (inputDir != Vector2.zero)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);


        running = (Input.GetKey(KeyCode.LeftShift));

        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;

        speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeedVelocity, smoothSpeedTime);

        //aumentando a aceleração da gravidade
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = transform.forward * speed * inputDir.magnitude + Vector3.up * velocityY; ;

        charController.Move(velocity * Time.deltaTime);

        speed = new Vector2(charController.velocity.x, charController.velocity.z).magnitude;

        if (charController.isGrounded)
        {
            velocityY = 0;
        }


        Jump();
        float animationSpeedPercent = ((running) ? speed / runSpeed : speed / walkSpeed * 0.5f) * inputDir.magnitude;  // o animationSpeedPercent é uma função?, sendo q ele esta sendo chamado para atualizar o valor do speed percent?


        //animator.SetFloat("speedPercent", animationSpeedPercent, smoothSpeedTime, Time.deltaTime);// "speedPercent" serve para acessar a blend tree                                                   //o animationSpeedPercent é o valor que se quer na variavel float, o valor q se quer vem sempre depois do nome do parametro 
                                                                                                  //o que vem depois do nome do parametro é o valor q se quer passar ao parametro, e como esta dentro do update vai sempre sendo atualizado
        //animator.SetBool("isground", charController.isGrounded); //o isground faz parte  da propriedade do CharacterController  // quando se fala de uma propriedade se refere a classe
        //animator.SetFloat("velocityY", velocityY);
        //animator.SetBool("spacedown", Input.GetKeyDown(KeyCode.Space));                                                                        // o "SpeedPercent" é o nome do parametro, um parametro é uma variavel que a condição usa;     
    }                                                                                                                                             //a condição é a regra 
                                                                                                                                                  //a transição é quando se troca de estados
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            if (charController.isGrounded)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Animations"))
                {
                    animator.Play("Jump up");
                }
                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;
            }

        }

    }

    //logica para andar sem rotacionar
    void walkSideways()
    {
        // pegar input do jogador
        // Input.getaxis retorna um valor de -1 a 1
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //normalizamos para pegar apenas a direção

        Vector3 direction = input.normalized;
        //nossa velocidade será a direção multiplicada pela nossa velocidade
        Vector3 velocity = direction * speed;

        //por fim a distância para percorrer será essa velocidade multiplicada pelo tempo
        Vector3 moveAmount = velocity * Time.deltaTime;

        //aqui iremos mover nosso jogador pela distância que iremos percorrer
        transform.Translate(moveAmount);
    }

    






    // o trigger existe em todos os colliders
    //o trigger ligado é para o personagem atravessar a a malha e continuar caindo, se não iria parra no ar 
    private void OnTriggerEnter(Collider other)//o other vai ser o colisor do objeto q colidimos   // aqui é para saber quando colidiu com o trigger, para saber se colidiu com a bala ou com o cubo que é o colisor de gameover ou com a moeda
    {
        if (other.transform.CompareTag("Coin")) // aqui é para ter certeza se a tag q colidiu é o coin   //o (other.transform.CompareTag("Coin")) serve para saber se o outro objeto tem a tag coin
        {                                         // o other vai ser sempre a colisão do outro objeto(não o player)

            if (other.GetComponent<Coin>().eAzul == true) // o Get component esta direcionando para a classe coin do outro objeto, e verificando se variavel eAzul é true ou false
            {                                                 //se a moeda pega for azul adcionamos +1 no contador de moedas azuis  //quando aparecer um ponto mais um nome depois dos parenteses, esta checando a variavel  
                //moedaPrefab.GetComponent<Coin>() == false // neste caso sem o ponto e nome, verifica se tem o componente ou não   
                                                              
                coinCountAzul++;
            }
            else // é amarelo
            {
                coinCountAmarelo++;
            }


            if (coinCountAzul % 5 == 0)
            {
                nivelCasa++;

                game_ref.SpawnarMoedasNoNivel(nivelCasa, game_ref.coinPrefabAzul);// o game_ref.coinPrefabAzul é para pegar a variavel q esta no game
            }

            hp++;
            textoazul.text = "Coins: " + coinCountAzul;
            textoamarelo.text = "Coins: " + coinCountAmarelo;
            textoHP.text = "HP: " + hp;

            //chamamos função do game para atualizar a lista de coins
            game_ref.RemoveCoinFromList(other.gameObject);
            GameObject.Destroy(other.gameObject);

            if (game_ref.listCoins.Count == 80) //quando chegar em 80 moedas pegas vai começar
            {

                for (int i = 0; i < game_ref.quantityOfEnemies; i++)
                {
                    if (game_ref.spawnPointsMoedas.Any()) ; //aqui menciona se tiver qualquer elemento na list de spawn points, mas ela começa vazia? 
                    {
                        int lugar = Random.Range(0, game_ref.spawnPointsMoedas.Count - 1); //aqui começa a verificação no zero e vai até o 9, por isso o -1? 

                    }


                }



            }

        }
        else if (other.transform.CompareTag("Bullet"))
        {
            hp--;

            if (hp <= 0)
            {
                textoGameover.text = "Game Over!";
            }
            textoHP.text = "HP: " + hp;

            GameObject.Destroy(other.gameObject);
        } else if (other.transform.CompareTag("Gameover"))
        {
            hp = 0;

            if (hp <= 0)
            {
                textoGameover.text = "Game Over!";
            }
            textoHP.text = "HP: " + hp;

            GameObject.Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision) //se o istrigger não estiver ligado
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            hp--;

            if (hp <= 0)
            {
                textoGameover.text = "Game Over!";
            }
            textoHP.text = "HP: " + hp;

            GameObject.Destroy(collision.gameObject);
        }

    }
}