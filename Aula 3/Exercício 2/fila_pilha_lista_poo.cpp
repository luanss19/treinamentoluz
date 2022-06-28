#include <iostream>
using namespace std;

class No
{
private:
  int valor;
  No *proximo, *anterior;

public:
  No(){};

  No(int valor)
  {
    proximo = NULL;
    valor = valor;
  };
  // getters
  int getValor()
  {
    return valor;
  }
  No *getProximo()
  {
    return proximo;
  }

  No *getAnterior()
  {
    return anterior;
  }

  // setters
  void setValor(int NovoValor)
  {
    if (NovoValor >= 0)
      valor = NovoValor;
  }

  void setProximo(No *NovoProximo)
  {
    if (NovoProximo != NULL)
      proximo = NovoProximo;
  }

  void setAnterior(No *NovoAnterior)
  {
    if (NovoAnterior != NULL)
      anterior = NovoAnterior;
  }

  No *criaNo(int valor)
  {
    No *novoNo = new No(valor);
    novoNo->setValor(valor);
    novoNo->setProximo(NULL);
    novoNo->setAnterior(NULL);
    return novoNo;
  }
};

class Lista
{

private:
  int tam;
  No *inicio, *fim, *topo;

public:
  Lista()
  {
    inicio = NULL;
    fim = NULL;
    topo = NULL;
    tam = 0;
  };

  // getters
  int getTamanho()
  {
    return tam;
  }

  No *getInicio()
  {
    return inicio;
  }

  No *getFim()
  {
    return fim;
  }

  No *getTopo()
  {
    return topo;
  }

  // setters

  void setTopo(No *NovoTopo)
  {
    if (NovoTopo != NULL)
      topo = NovoTopo;
  }

  void setTamanho(int tamanho)
  {
    if (tamanho >= 0)
      tam = tamanho;
  }

  void setInicio(No *NovoInicio)
  {
    if (NovoInicio != NULL)
      inicio = NovoInicio;
  }

  void setFim(No *NovoFim)
  {
    if (NovoFim != NULL)
      fim = NovoFim;
  }

  // funções

  Lista *criaLista()
  {
    Lista *novaLista = new Lista();
    novaLista->setInicio(NULL);
    novaLista->setFim(NULL);
    novaLista->setTamanho(0);
    return novaLista;
  }

  void inserirInicio(Lista *lista, int valor)
  {
    No *novo;
    novo = novo->criaNo(valor);
    if (lista->inicio == NULL)
    { // vê se a lista tá vazia
      lista->inicio = novo;
      lista->fim = novo;
    }
    else
    {
      No *aux;
      aux = aux->criaNo(lista->getInicio()->getValor());
      aux = lista->getInicio();
      novo->setProximo(aux);
      aux->setAnterior(novo);
      lista->setInicio(novo);
    }
    // aumenta o tamanho da lista
    lista->tam++;
  }

  void InserirFim(Lista *lista, int valor)
  {
    // crira novo nó alocando o espaço no heap com o malloc, do tamanho de um nó
    No *novo;
    novo = novo->criaNo(valor);
    if (lista->inicio == NULL)
    { // vê se a lista tá vazia
      lista->inicio = novo;
      lista->fim = novo;
    }
    else
    {
      No *aux;
      aux = aux->criaNo(lista->fim->getValor());
      aux = lista->fim;
      novo->setAnterior(aux);
      aux->setProximo(novo);
      lista->setFim(novo);
    }
    // aumenta o tamanho da lista
    lista->tam++;
  }

  void removerElemento(Lista *lista, int pos)
  {
    int i;
    No *aux, *aux2;
    aux = aux->criaNo(0);
    aux2 = aux->criaNo(0);

    if (lista->tam == 0)
      return;

    if (pos == 1)
    { // se for o primeiro elemento
      aux = lista->getInicio();
      lista->setInicio(lista->inicio->getProximo());
      if (lista->inicio == NULL)
        lista->fim = NULL;
      else
        lista->inicio->setAnterior(NULL);
    }
    else if (pos == lista->tam)
    { // se for o último elemento
      aux = lista->fim;
      lista->fim->getAnterior()->setProximo(NULL);
      lista->fim = lista->fim->getAnterior();
    }
    else
    { // se for em outro lugar da lista
      aux2 = lista->inicio;
      for (i = 1; i < pos; ++i)
        aux2 = aux2->getProximo();
      aux = aux2;
      aux2->getAnterior()->setProximo(aux2->getProximo());
      aux2->getProximo()->setAnterior(aux2->getAnterior());
    }
    free(aux);
    lista->tam--;
  }

  void imprimir(Lista *lista)
  {
    No *inicio = lista->inicio;
    cout << "\nTamanho da lista: " << lista->tam << "\n";
    while (inicio != NULL)
    {
      printf("%d ", inicio->getValor());
      inicio = inicio->getProximo();
    }
    printf("\n\n");
  }
};

class Pilha : public Lista
{
private:
  int tam;
  No *topo;

public:
  Pilha()
  {
    topo = NULL;
    tam = 0;
  };

  // funções

  Pilha *criaPilha()
  {
    Pilha *novaPilha = new Pilha();
    novaPilha->setTopo(NULL);
    novaPilha->setTamanho(0);
    return novaPilha;
  }

  void imprimir(Pilha *pilha)
  {
    No *inicio = pilha->getTopo();
    cout << "\nTamanho da pilha: " << pilha->getTamanho() << "\n";
    while (inicio != NULL)
    {
      printf("%d \n", inicio->getValor());
      inicio = inicio->getProximo();
    }
    printf("\n\n");
  }

  void InserirFim(Pilha *pilha, int valor)
  {
    No *aux;
    aux = aux->criaNo(valor);
    aux->setProximo(pilha->getTopo());
    pilha->setTopo(aux);
    pilha->setTamanho(pilha->getTamanho() + 1);
  }

  int RetirarFim(Pilha *pilha)
  {
    No *aux;
    aux = aux->criaNo(pilha->getTopo()->getValor());
    int v;

    if (pilha->getTopo() == NULL)
    {
      printf("Lista vazia\n");
      return 0;
    }
    aux = pilha->getTopo();
    pilha->setTopo(aux->getProximo());
    v = aux->getValor();
    pilha->setTamanho(pilha->getTamanho() - 1);
    // free(aux);
    return v;
  }
};

class Fila : public Lista
{
private:
  int tam;
  No *inicio, *fim;

public:
  Fila()
  {
    inicio = NULL;
    fim = NULL;
    tam = 0;
  };

  Fila *criaFila()
  {
    Fila *novaFila = new Fila();
    novaFila->setInicio(NULL);
    novaFila->setFim(NULL);
    novaFila->setTamanho(0);
    return novaFila;
  }

  void InserirFim(Fila *fila, int valor)
  {
    No *aux;
    aux = aux->criaNo(valor);

    if (fila->getFim() != NULL) // fila não está vazia
    {
      fila->getFim()->setProximo(aux);
    }
    else // fila vazia
    {
      fila->setInicio(aux);
    }

    fila->setFim(aux);
    fila->setTamanho(fila->getTamanho() + 1);
  }

  int RemoverInicio(Fila *fila)
  {
    No *aux;
    aux = aux->criaNo(fila->inicio->getValor());
    int v;
    if (fila->inicio == NULL)
    {
      printf("Fila vazia\n");
      return 0;
    }

    aux = fila->inicio;
    v = aux->getValor();
    fila->inicio = aux->getProximo(); // inicio da fila recebe o segundo elemento

    if (fila->inicio == NULL)
    {
      fila->fim = NULL;
    }
    fila->tam--;

    // free(aux);
    return v;
  }

  void imprimir(Fila *fila)
  {
    No *inicio = fila->getInicio();
    cout << "\nTamanho da fila: " << fila->getTamanho() << "\n";
    while (inicio != NULL)
    {
      printf("%d ", inicio->getValor());
      inicio = inicio->getProximo();
    }
    printf("\n\n");
  }
};

void TesteLista()
{
  Lista *lista = lista->criaLista();
  int size, i;
  cout << "Digite o tamanho da lista:";
  scanf("%d", &size);
  for (i = 0; i < size; i++)
    lista->inserirInicio(lista, rand() % size);

  lista->imprimir(lista);

  lista->inserirInicio(lista, 55);
  printf("Inserido no inicio: 55 \n");

  lista->imprimir(lista);

  lista->InserirFim(lista, 66);
  printf("Inserido no fim: 66 \n");

  lista->imprimir(lista);

  lista->removerElemento(lista, 1);
  printf("Lista com o primeiro elemento removido: \n");

  lista->imprimir(lista);

  printf("Lista com o quarto elemento removido: \n");

  lista->removerElemento(lista, 4);

  lista->imprimir(lista);
};

void TestePilha()
{
  Pilha *pilha = pilha->criaPilha();

  int size, i;
  printf("Digite o tamanho da pilha: ");
  scanf("%d", &size);
  for (i = 0; i < size; i++)
    pilha->InserirFim(pilha, rand() % size);

  pilha->imprimir(pilha);
  printf("Inserido no topo: 99 \n");
  pilha->InserirFim(pilha, 99);

  pilha->imprimir(pilha);

  printf("Retirado do topo: 99 \n");
  pilha->RetirarFim(pilha);

  pilha->imprimir(pilha);
}

void TesteFila()
{
  Fila *fila = fila->criaFila();

  int size, i;
  printf("Digite o tamanho da fila: ");
  scanf("%d", &size);
  for (i = 0; i < size; i++)
    fila->InserirFim(fila, rand() % size);

  fila->imprimir(fila);

  fila->InserirFim(fila, 55);
  printf("Inserido no fim: 55");

  fila->imprimir(fila);

  fila->InserirFim(fila, 66);
  printf("Inserido no fim: 66");

  fila->imprimir(fila);

  for (i = fila->getTamanho(); i > 0; i--)
  {
    printf("Retirado o elemento %d do inicio", fila->getInicio()->getValor());
    fila->RemoverInicio(fila);

    fila->imprimir(fila);
  }
}

int main()
{
  TesteLista();
  printf("---------------------------------------------------------------------------\n");
  TestePilha();
  printf("---------------------------------------------------------------------------\n");
  TesteFila();
}