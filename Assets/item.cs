using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //para q serve o UnityEngine.EventSystems?

public class Item : MonoBehaviour, IDragHandler, IEndDragHandler //o IDragHandler e o IEndDragHandler servem para quando o player arrastar os itens do inventario, ele pega-los? 
{
    private Vector3 LastTransform;


    public void OnDrag(PointerEventData eventData)
    {
           
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        int t = -1;
        Slot[] slots = FindObjectsOfType<Slot>(); //o q significa aqui a espressão importar?  //aqui esta procurando objetos do tipo slot e salvando na variavel slots?esta parte é um array?
        for(int i=0; i<slots.Length; i++)   //o q é o slots.Length?
        {
            if(slots[i].selected==true)  //o [i]serve para representar a posição do objeto do tipo Slot? //o q quer dizer o slots[i].selected==true? 
            {
                t = i;
                break;  //este break é pq não tem um else depois deste if?
            }                


        }   
        if(t>=0) //
        {
            transform.position = slots[i].transform.position; //?
            LastTransform = transform.position;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        LastTransform = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
