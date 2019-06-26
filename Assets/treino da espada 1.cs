using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    public float jumpheight;
    public int coincountAzul;
    public int coincountAmarela;
    public int niveldaCasa;

    public int hp;
    public float walkspeed;
    public float runspeed;
    public float gravity;

    public float timer;
    public float timerMax;

    public TextMeshProUGUI textoHP;
    public TextMeshProUGUI textoAmarelo;
    public TextMeshProUGUI textoAzul;
    public TextMeshProUGUI textoGameover;

    [SerializeField]
    float smoothrotationtime;

    [SerializeField]
    float smoothrotationvelocity;

    [SerializeField]
    float smoothspeedTime;

    [SerializeField]
    float smoothspeedVelocity;

    [SerializeField]
    bool isoncooldown;

    [SerializeField]
    private Transform CameraT;

    void Start()
    {
        CameraT = Camera.main.transform;

        charController = GetComponent<CharacterController>();

        Animator = GetComponent<Animator>();


        textoMoedasAmarelas.Text = "moedas Amarelas: " + coincountAmarela;
        textoMoedasAzuis.Text = "moedas Azuis: " + coincountAzul;
        textoHP.text = "HP: " + hp;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void andarRotacionando()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("horizontal"), Input.GetAxisRaw("vertical"));
        Vector2 inputDir = input.normalized;

        float TargetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + CameraT.eulerAngles.y;





    }