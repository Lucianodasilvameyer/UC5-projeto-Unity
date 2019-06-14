using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    //parte diferente
    public float speed = 2f;
    public float jumpHeight = 5f;
    public int coinCountAmarelo = 0;
    public int coinCountAzul;

    public int hp = 10;

    public float walkSpeed = 3f;
    public float runSpeed = 8f;
    public float gravity = -12f;

    [SerializeField]
    float velocityY;


    [SerializeField]
    bool running = false;


    [SerializeField]
    Game game_ref;
                               //**para fazer referencias**
    public MeshRenderer mesh; //ao criar uma variavel meshrenderer no script do player, aparecera no inspector o seu nome no caso "mesh", dai é só arrastar o object da hierarchy o qual quer usar o seu "meshrenderer" para o script na parte escrita "mesh"?sim.o mesh renderer é tanto a classe do mesh quanto a classe q se quer usar do plane?sim


    public TextMeshProUGUI textoamarelo; // aqui todas as variaveis aparecem no inspector
    public TextMeshProUGUI textoazul;
    public TextMeshProUGUI textoHP;

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



        //Instantiate(coinPrefab, new Vector3(2, 2, 2), Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {

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


        animator.SetFloat("speedPercent", animationSpeedPercent, smoothSpeedTime, Time.deltaTime);// "speedPercent" serve para acessar a blend tree                                                   //o animationSpeedPercent é o valor que se quer na variavel float, o valor q se quer vem sempre depois do nome do parametro 
                                                                                                  //o que vem depois do nome do parametro é o valor q se quer passar ao parametro, e como esta dentro do update vai sempre sendo atualizado
        animator.SetBool("isground", charController.isGrounded); //o isground faz parte  da propriedade do CharacterController  // quando se fala de uma propriedade se refere a classe
        animator.SetFloat("velocityY", velocityY);
        animator.SetBool("spacedown", Input.GetKeyDown(KeyCode.Space));                                                                        // o "SpeedPercent" é o nome do parametro, um parametro é uma variavel que a condição usa;     
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

    private void OnTriggerEnter(Collider other)//o other vai ser o colisor do objeto q colidimos
    {
        if (other.transform.CompareTag("Coin")) // aqui é para ter certeza se a tag q colidiu é o coin
        {                                         // o other vai ser sempre a colisão do outro objeto(não o player)

            if (other.GetComponent<Coin>().eAzul == true) // é azul
            {

                coinCountAzul++;
            }
            else // é amarelo
            {
                coinCountAmarelo++;
            }
            
           
            hp++;
            textoazul.text = "Coins: " + coinCountAzul;
            textoamarelo.text = "Coins: " + coinCountAmarelo;
            textoHP.text = "HP: " + hp;

            //chamamos função do game para atualizar a lista de coins
            game_ref.RemoveCoinFromList(other.gameObject);
            GameObject.Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            hp--;
            textoHP.text = "HP: " + hp;

            GameObject.Destroy(collision.gameObject);
        }
    }
}