using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //para q serve o UnityEngine.EventSystems?  //o q significa aqui a espressão importar?

public class Item : MonoBehaviour, IDragHandler, IEndDragHandler //o IDragHandler e o IEndDragHandler servem para quando o player arrastar os itens do inventario? mas eles tb colocam os itens em outro lugar? 
{
    private Vector3 LastTransform;
    public int id;//id?
    public Slot inSlot;
    
   
    public void OnDrag(PointerEventData eventData)//?
    {
        transform.position = Input.mousePosition;//?    
    }


    public void OnEndDrag(PointerEventData eventData)
    {                                                         //o q significa aqui a espressão importar?
        int t = -1;
        Slot[] slots = FindObjectsOfType<Slot>();   //aqui esta procurando objetos do tipo slot e salvando na variavel slots?  //esta parte é um array? //como se sabe se um objeto é do tipo Slot?
                                        
                                            //este Slot com s maiusculo é apenas o tipo de variavel q o slot é? não é o outro script?

        for(int i=0; i<slots.Length; i++)   //o q é o slots.Length?
        {
            if(slots[i].selected==true)  //o [i]serve para representar a posição do objeto do tipo Slot? //o q quer dizer o slots[i].selected==true? 
            {
                t = i;// se t é igual a i, então daria no mesmo escrever if(slots[t].selected==true)?
                break;  //este break é pq não tem um else depois deste if?
            }                


        }   
        if(t>=0) // ?
        {
            if (slots[t].id == -1)//aqui o slots[t] se refere a uma posição expecifica no array slots?  //para q serve o .id==-1?  // o -1 sgnifica q essa posição esta vazia, mas não posso usar 0 no lugar do -1?
            {
                inSlot.id = -1;
                inSlot.Count = 0; //pq aqui não aceita o count com c minusculo?
                transform.position = slots[t].transform.position; //?
                LastTransform = transform.position;//?
                inSlot = slots[t];
                slots[t].id = id;//?
            }
            else
            {
                transform.position = LastTransform;
            }
        }
        else
        {
            transform.position = LastTransform;
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
