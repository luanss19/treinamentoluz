#include <stdio.h>
#include <stdlib.h>

// definição da estrutura do nó
typedef struct No
{
    int valor;
    struct No *anterior;
    struct No *proximo;
} No;

// definição da estrutura da lista
typedef struct
{
    No *inicio, *fim;
    int tam;
} Lista;




Lista* criaLista (){
    Lista *novaLista = malloc(sizeof(Lista));
    novaLista->inicio = NULL;
    novaLista->fim = NULL;
    novaLista->tam = 0;
    return novaLista;
}

No* criaNo (int valor){
    No *novoNo = (No *)malloc(sizeof(No));
    novoNo->valor = valor;
    novoNo->proximo = NULL;
    novoNo->anterior = NULL;
    return novoNo;
}



// inserir o nó no início da lista. recebe a lista e o valor a ser adicionado
// complexidade = com vetor O(n) / com lista O(1)
void inserirInicio(Lista *lista, int valor)
{
    // crira novo nó alocando o espaço no heap com o malloc, do tamanho de um nó
    No *novo = criaNo(valor);
    if (lista->inicio == NULL)
    { // vê se a lista tá vazia
        lista->inicio = novo;
        lista->fim = novo;
    }
    else
    {
        No *aux = criaNo(lista->inicio->valor);
        aux = lista->inicio;
        novo->proximo = aux;
        aux->anterior = novo;
        lista->inicio = novo;
    }
    // aumenta o tamanho da lista
    lista->tam++;
}

void imprimirLista(Lista *lista)
{
    No *inicio = lista->inicio;
    printf("\nTamanho da lista: %d\n", lista->tam);
    while (inicio != NULL)
    {
        printf("%d ", inicio->valor);
        inicio = inicio->proximo;
    }
    printf("\n\n");
}

void removerElemento(Lista *lista, int pos)
{
    int i;   
  No *aux,*aux2 = criaNo(0);

  if(lista->tam == 0)   
    return;   

  if(pos == 1){ //se for o primeiro elemento
    aux = lista->inicio;   
    lista->inicio = lista->inicio->proximo;   
    if(lista->inicio == NULL)   
      lista->fim = NULL;   
    else   
      lista->inicio->anterior == NULL;   
  }else if(pos == lista->tam){ //se for o último elemento
    aux = lista->fim;   
    lista->fim->anterior->proximo = NULL;   
    lista->fim = lista->fim->anterior;   
  }else { //se for em outro lugar da lista
    aux2 = lista->inicio;   
      for(i=1;i<pos;++i)   
        aux2 = aux2->proximo;   
    aux = aux2;   
    aux2->anterior->proximo = aux2->proximo;   
    aux2->proximo->anterior = aux2->anterior;   
  }   
  free(aux);   
  lista->tam--;
}

void InserirFim(Lista *lista, int valor)
{
    // crira novo nó alocando o espaço no heap com o malloc, do tamanho de um nó
    No *novo = criaNo(valor);
    if (lista->inicio == NULL)
    { // vê se a lista tá vazia
        lista->inicio = novo;
        lista->fim = novo;
    }
    else
    {
        No *aux = criaNo(lista->fim->valor);
        aux = lista->fim;
        novo->anterior = aux;
        aux->proximo = novo;
        lista->fim = novo;
    }
    // aumenta o tamanho da lista
    lista->tam++;
}

void main()
{
    Lista *lista = criaLista();
    

    int size, i;
    printf("Digite o tamanho da lista: ");
    scanf("%d", &size);
    for (i = 0; i < size; i++)
        inserirInicio(lista, rand() % size);

    imprimirLista(lista);

    inserirInicio(lista, 55);

    imprimirLista(lista);

    InserirFim(lista, 66);

    imprimirLista(lista);

    removerElemento(lista, 1);
    imprimirLista(lista);


    removerElemento(lista, 4);

    imprimirLista(lista);



}