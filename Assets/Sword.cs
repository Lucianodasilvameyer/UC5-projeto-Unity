using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int dano;
    public float timer;
    public float timerMax = 2;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= timer + timerMax)
        {

            timer = Time.time;
            gameObject.SetActive(false);
            
        }

    }                                                        
                                 
                                  
                                   //dentro de if o GetComponent verifica se uma classe esta dentro de um game object
                                 //se o GetComponent não estiver dentro do if e estiver depois de um sinal de igual ele esta salvando(colocando a classe) em uma variavel
                                
    private void OnTriggerEnter(Collider other) // o Collider other se refere á colisão do objeto com o qual se vai colidir?           
    {
        if (other.CompareTag("Enemy")) //aqui esta comparando pela tag se o objeto q esta colidindo com a espada é um inimigo? sim             
        {
            //Inimigo inimigoColidiu = other.GetComponent<Inimigo>();
            //inimigoColidiu.TomarDano(dano);

            other.GetComponent<Inimigo>().TomarDano(dano);//neste caso o other é usado por q queremos lidar com o objeto q colidimos, 

          
        }
    }

}
