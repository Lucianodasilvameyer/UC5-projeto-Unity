using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//para q serve o using UnityEngine.UI? 

public class Slot: MonoBehaviour
{
    public int id = -1; //-1 quer diser q não tem itens nele
    public int Count=0; // aqui significa q não tem nenhum item dentro do inventório?
    public bool selected = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     private void Update()
     {
        selected = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition); //se estiver com o mouse por cima do slot o selected sera igual a true? senão sera igual a false?
       
            
            
     }
}
