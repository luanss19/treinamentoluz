#include <stdio.h>
#include <stdlib.h>

// definição da estrutura do nó
typedef struct No
{
    int valor;
    struct No *proximo;
} No;

// definição da estrutura da pilha
typedef struct
{
    No *topo;
    int tam;
} Pilha;




Pilha* criaPilha (){
    Pilha *novaPilha = malloc(sizeof(Pilha));
    novaPilha->topo = NULL;
    novaPilha->tam = 0;
    return novaPilha;
}

No* criaNo (int valor){
    No *novoNo = (No *)malloc(sizeof(No));
    novoNo->valor = valor;
    novoNo->proximo = NULL;
    return novoNo;
}




void imprimirPilha(Pilha *pilha)
{
    No *inicio = pilha->topo;
    printf("\nTamanho da pilha: %d\n", pilha->tam);
    while (inicio != NULL)
    {
        printf("%d ", inicio->valor);
        inicio = inicio->proximo;
    }
    printf("\n\n");
}

void liberaPilha(No * * pilha){
  No * aux = * pilha; 
  No * temp;          
  while (aux){            
    temp = aux;           
    aux = aux -> proximo;    
    free(temp);           
  }
  * pilha = NULL;         
}

void InserirFim(Pilha *pilha, int valor){
    No *aux = criaNo(valor);

    aux->proximo = pilha->topo;
    pilha->topo = aux;
    pilha->tam++;
}

int RetirarFim(Pilha *pilha){
    No *aux = criaNo(pilha->topo->valor);
    int v;

    if(pilha->topo == NULL){
        printf("Lista vazia\n"); return 0;
    }
    aux = pilha->topo;
    pilha->topo = aux->proximo;
    v = aux->valor;
    pilha->tam--;
    // free(aux);
    return v;
}

void main()
{
    Pilha *pilha = criaPilha();

    int size, i;
    printf("Digite o tamanho da pilha: ");
    scanf("%d", &size);
    for (i = 0; i < size; i++)
        InserirFim(pilha, rand() % size);

    InserirFim(pilha, 99);

    imprimirPilha(pilha);

    
    RetirarFim(pilha);

    imprimirPilha(pilha);

}