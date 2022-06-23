#include <stdio.h>
#include <stdlib.h>

// definição da estrutura do nó
typedef struct No
{
    int valor;
    struct No *esq, *dir, *pai;
} No;

// definição da estrutura da arvore
typedef struct
{
    No *raiz;
    int tam;
} Arvore;

Arvore *criaArvore()
{
    Arvore *novaArvore = malloc(sizeof(Arvore));
    novaArvore->raiz = NULL;
    novaArvore->tam = 0;
    return novaArvore;
}

No *criaNo(int valor)
{
    No *novoNo = (No *)malloc(sizeof(No));
    novoNo->valor = valor;
    novoNo->esq = NULL;
    novoNo->dir = NULL;
    novoNo->pai = NULL;
    return novoNo;
}

void inserirDireita(No *raiz, int valor);

void inserirEsquerda(No *raiz, int valor)
{
    if (raiz->esq == NULL)
    {
        No *novoNo = criaNo(valor);
        raiz->esq = novoNo;
        raiz->esq->pai = raiz;
    }
    else
    {
        if (valor < raiz->esq->valor)
            inserirEsquerda(raiz->esq, valor);
        if (valor > raiz->esq->valor)
            inserirDireita(raiz->esq, valor);
    }
}

void inserirDireita(No *raiz, int valor)
{
    if (raiz->dir == NULL)
    {
        No *novoNo = criaNo(valor);
        raiz->dir = novoNo;
        raiz->dir->pai = raiz;
    }
    else
    {
        if (valor > raiz->dir->valor)
            inserirDireita(raiz->dir, valor);
        if (valor < raiz->dir->valor)
            inserirEsquerda(raiz->dir, valor);
    }
}

void InserirNoArvore(Arvore *arvore, int valor)
{
    if (arvore->raiz == NULL)
    {
        No *novoNo = criaNo(valor);
        arvore->raiz = novoNo;
        arvore->tam++;
    }
    else
    {
        if (valor < arvore->raiz->valor)
        {
            inserirEsquerda(arvore->raiz, valor);
            arvore->tam++;
        }
        if (valor > arvore->raiz->valor)
        {
            inserirDireita(arvore->raiz, valor);
            arvore->tam++;
        }
    }
}

void imprimirArvoreEm_Ordem(No *raiz)
{
    if (raiz != NULL)
    {
        imprimirArvoreEm_Ordem(raiz->esq);
        printf("%d \n", raiz->valor);
        imprimirArvoreEm_Ordem(raiz->dir);
    }
}

void imprimirArvorePre_Ordem(No *raiz)
{
    if (raiz != NULL)
    {
        printf("%d \n", raiz->valor);
        imprimirArvorePre_Ordem(raiz->esq);
        imprimirArvorePre_Ordem(raiz->dir);
    }
}

void imprimirArvorePos_Ordem(No *raiz)
{
    if (raiz != NULL)
    {
        imprimirArvorePos_Ordem(raiz->esq);
        imprimirArvorePos_Ordem(raiz->dir);
        printf("%d \n", raiz->valor);
    }
}

int buscarNaArvore(No *raiz, int chave) {
    if(raiz == NULL) {
        return 0;
    } else {
        if(raiz->valor == chave)
            return 1;
        else {
            if(chave < raiz->valor)
                return buscarNaArvore(raiz->esq, chave);
            else
                return buscarNaArvore(raiz->dir, chave);
        }
    }
}

No* removerNo(No *raiz, int valorRemover) {
    if(raiz == NULL) {
        printf("Valor nao encontrado na arvore/arvore sem valores\n");
        return NULL;
    } else {
        if(raiz->valor == valorRemover) {
            // remove o nó sem filho
            if(raiz->esq == NULL && raiz->dir == NULL) {
                if(raiz->pai->esq == raiz)
                    raiz->pai->esq = NULL;

                if(raiz->pai->dir == raiz)
                    raiz->pai->dir = NULL;
                free(raiz);
                return NULL;
            }
            else{
                // remover nó com 1 filho só
                if(raiz->esq == NULL || raiz->dir == NULL){
                    No *aux;
                    if(raiz->esq != NULL){
                        aux = raiz->esq;
                        raiz->pai->esq=aux;
                    }
                        
                    else {
                        aux = raiz->dir;
                        raiz->pai->dir=aux;

                    }
                    free(raiz);
                    return aux;
                }
                else{
                    No *aux = raiz->dir;
                    while(aux->esq != NULL)  // vai até o valor mais proximo (a direita) do nó a ser removido
                        aux = aux->esq;


                    //pai do antigo no aponta pro novo
                    raiz->pai->esq = aux;

                    //filhos apontam pro novo no
                    raiz->esq->pai=aux;
                    raiz->dir->pai=aux;

                    //novo no aponta pro pai do antigo no e novo no recebe o valor do antigo
                    aux->pai = raiz->pai;
                    aux->valor = raiz->valor;
                    free(raiz);
                    return aux;
                }
            }
        } else {
            if(valorRemover < raiz->valor)
                raiz->esq = removerNo(raiz->esq, valorRemover); // recursão indo pra esquerda até achar o valor
            else
                raiz->dir = removerNo(raiz->dir, valorRemover); // recursão indo pra direita até achar o valor
            return raiz;
        }
    }
}


int buscar(No *raiz, int chave) {
    if(raiz == NULL) {
        return 0;
    } else {
        if(raiz->valor == chave)
            return raiz->valor;
        else {
            if(chave < raiz->valor)
                return buscar(raiz->esq, chave);
            else
                return buscar(raiz->dir, chave);
        }
    }
}

void liberaNo(No* noRaiz) {
if (noRaiz) {
  liberaNo(noRaiz->esq);
  liberaNo(noRaiz->dir);
  free(noRaiz);
  noRaiz = NULL;
 }
}

void liberaArvore(Arvore* arvore) {
if (arvore) {
  liberaNo(arvore->raiz);
  free(arvore);
 }
}


void main()
{
    Arvore *arvore = criaArvore();
    int op, valor;
    do
    {
        printf("\n0 - Sair\n1 - Inserir\n2 - Imprimir em ordem\n3 - Imprimir em pre ordem\n4 - Imprimir em pos ordem\n5 - Buscar\n6 - Remover Dado\n7 - Liberar Lista\n");
        scanf("%d", &op);

        switch (op)
        {
        case 0:
            printf("\n...\n");
            break;
        case 1:
            printf("Digite um valor a ser inserido na Arvore: ");
            scanf("%d", &valor);
            InserirNoArvore(arvore, valor);
            break;
        case 2:
            printf("\nImpressao da arvore binaria em Ordem (raiz impressa entre as sub-arvores):\n");
            imprimirArvoreEm_Ordem(arvore->raiz);
            printf("\n");
            printf("Tamanho: %d\n", arvore->tam);
            break;
        case 3:
            printf("\nImpressao da arvore binaria Pre Ordem (raiz impressa antes das sub-arvores):\n");
            imprimirArvorePre_Ordem(arvore->raiz);
            printf("\n");
            printf("Tamanho: %d\n",arvore->tam);
            break;
        case 4:
            printf("\nImpressao da arvore binaria em Pos Ordem (raiz impressa depois das sub-arvores):\n");
            imprimirArvorePos_Ordem(arvore->raiz);
            printf("\n");
            printf("Tamanho: %d\n",arvore->tam);
            break;
            case 5:
                printf("Qual valor deseja buscar? ");
                scanf("%d", &valor);
                printf("Resultado da busca: %d\n", buscar(arvore->raiz, valor));
                break;
         case 6:
             printf("Digite um valor a ser removido: ");
             scanf("%d", &valor);
             arvore->raiz = removerNo(arvore->raiz, valor);
             break;
        case 7:
             printf("\n Arvore limpa da memoria \n");
             liberaArvore(arvore);
             break;
        default:
            printf("\nOpcao invalida\n");
        }
    } while (op != 0);
}