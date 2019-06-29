using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int dano;
    public float timer;
    public float timerMax;  //este é o tempo de vida da espada
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time; //para q serve este timer do void start?
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= timer + timerMax) // o time.time é só o relogio, o timer é o tempo inicial q ficou guardado e o timemax é o tempo de duração da espada, o timer + timerMax guardam o tempo exato para o compromisso 
        {                                  //aqui o > é para garantir q entre dentro do if caso ocorra um atraso no compromisso?sim

           
            gameObject.SetActive(false); //aqui a espada é destruida
            
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
              //aqui esta pegando o script inimigo do objeto q colidimos
              // como se trata do outro inimigo tomar dano se usa o get component
        }     //pq o q quer q tome dano seja o inimigo
    }                                                       //o dano em parenteses se refere a quantidade de dano q é mostrada no inspector 

}
