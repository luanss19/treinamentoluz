#include <stdio.h>
#include <stdlib.h>

// definição da estrutura do nó
typedef struct No
{
    int valor;
    struct No *proximo;
} No;

// definição da estrutura da fila
typedef struct
{
    No *inicio, *fim;
    int tam;
} Fila;

Fila *criaFila()
{
    Fila *novaFila = malloc(sizeof(Fila));
    novaFila->inicio = NULL;
    novaFila->fim = NULL;
    novaFila->tam = 0;
    return novaFila;
}

No *criaNo(int valor)
{
    No *novoNo = (No *)malloc(sizeof(No));
    novoNo->valor = valor;
    novoNo->proximo = NULL;
    return novoNo;
}

void imprimirFila(Fila *fila)
{
    No *inicio = fila->inicio;
    printf("\nTamanho da fila: %d\n", fila->tam);
    while (inicio != NULL)
    {
        printf("%d ", inicio->valor);
        inicio = inicio->proximo;
    }
    printf("\n\n");
}

void InserirFim(Fila *fila, int valor)
{
    No *aux = criaNo(valor);

    if (fila->fim != NULL) // fila não está vazia
    {
        fila->fim->proximo = aux;
    }
    else // fila vazia
    {
        fila->inicio = aux;
    }

    fila->fim = aux;
    fila->tam++;
}

int RemoverInicio(Fila *fila)
{
    No *aux = criaNo(fila->inicio->valor);
    int v;
    if (fila->inicio == NULL)
    {
        printf("Fila vazia\n");
        return 0;
    }

    aux = fila->inicio;
    v = aux->valor;
    fila->inicio = aux->proximo; // inicio da fila recebe o segundo elemento

    if (fila->inicio == NULL)
    {
        fila->fim = NULL;
    }
    fila->tam--;

    // free(aux);
    return v;
}

void libera(No *fila)
{
 if(fila->proximo != NULL){
    No *proxNo,*atual;

  atual = fila->proximo;
  while(atual != NULL){
   proxNo = atual->proximo;
   free(atual);
   atual = proxNo;
  }
 }
}

void main()
{
    Fila *fila = criaFila();

    int size, i;
    printf("Digite o tamanho da fila: ");
    scanf("%d", &size);
    for (i = 0; i < size; i++)
        InserirFim(fila, rand() % size);

    imprimirFila(fila);

    InserirFim(fila, 55);

    imprimirFila(fila);

    InserirFim(fila, 66);

    imprimirFila(fila);

    RemoverInicio(fila);

    imprimirFila(fila);

    RemoverInicio(fila);

    imprimirFila(fila);

    RemoverInicio(fila);

    imprimirFila(fila);
}