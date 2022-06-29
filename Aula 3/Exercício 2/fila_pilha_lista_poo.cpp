#include <iostream>
using namespace std;

class No
{
private:
  int valor;
  No *proximo, *anterior;

public:
  No(){};

  No(int novo)
  {
    anterior = NULL;
    proximo = NULL;
    valor = novo;
  };

  ~No(){
    delete(anterior);
    delete(proximo);
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
};

class Lista
{

private:
  int tam;
  No *inicio, *fim;

public:
  Lista()
  {
    inicio = NULL;
    fim = NULL;
    tam = 0;
  };

  ~Lista(){
    delete(inicio);
    delete(fim);
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
  // setters

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

  void inserirInicio(int valor)
  {
    No *novo = new No(valor);
    if (this->inicio == NULL)
    {
      this->inicio = novo;
      this->fim = novo;
    }
    else
    {
      No *aux = new No(this->getInicio()->getValor());
      aux = this->getInicio();
      novo->setProximo(aux);
      aux->setAnterior(novo);
      this->setInicio(novo);
    }
    // aumenta o tamanho da lista
    this->tam++;
  }

  void InserirFim(int valor)
  {
    No *novo = new No(valor);
    if (this->inicio == NULL)
    {
      this->inicio = novo;
      this->fim = novo;
    }
    else
    {
      No *aux = new No(this->getFim()->getValor());
      aux = this->getFim();
      novo->setAnterior(aux);
      aux->setProximo(novo);
      this->setFim(novo);
    }
    // aumenta o tamanho da lista
    this->setTamanho(this->getTamanho() + 1);
  }

  void removerElemento(int pos)
  {
    int i;
    No *aux, *aux2 = new No(0);

    if (this->tam == 0)
      return;

    if (pos == 1)
    { // se for o primeiro elemento
      aux = this->getInicio();
      this->setInicio(this->inicio->getProximo());
      if (this->inicio == NULL)
        this->fim = NULL;
      else
        this->inicio->setAnterior(NULL);
    }
    else if (pos == this->tam)
    { // se for o último elemento
      aux = this->fim;
      this->fim->getAnterior()->setProximo(NULL);
      this->fim = this->fim->getAnterior();
    }
    else
    { // se for em outro lugar da lista
      aux2 = this->inicio;
      for (i = 1; i < pos; ++i)
        aux2 = aux2->getProximo();
      aux = aux2;
      aux2->getAnterior()->setProximo(aux2->getProximo());
      aux2->getProximo()->setAnterior(aux2->getAnterior());
    }
    this->tam--;
  }

  void imprimir()
  {
    No *inicio = this->getInicio();
    cout << "\nTamanho da estrutura: " << this->getTamanho() << "\n";
    while (inicio != NULL)
    {
      printf("%d ", inicio->getValor());
      inicio = inicio->getProximo();
    }
    printf("\n\n");
  }

  void RetirarFim()
  {
    No *aux = new No(this->getFim()->getValor());
    if (this->getInicio() == NULL)
    {
      printf("Estrutura vazia\n");
    }
    aux = this->getInicio();
    this->setInicio(aux->getProximo());
    this->setTamanho(this->getTamanho() - 1);
  }

  int RemoverInicio()
  {
    No *aux = new No(this->getInicio()->getValor());
    int v;
    if (this->getInicio() == NULL)
    {
      printf("Vazio \n");
      return 0;
    }

    aux = this->getInicio();
    v = aux->getValor();
    this->setInicio(aux->getProximo()); // inicio da fila recebe o segundo elemento

    if (this->getInicio() == NULL)
    {
      this->setFim(NULL);
    }
    this->setTamanho(this->getTamanho() - 1);
    return v;
  }
};

class Pilha
{
private:
  Lista *lista;

public:
  Pilha()
  {
    lista = new Lista();
  };

  ~Pilha(){
     delete(lista);
  };

  // funções

  void imprimir()
  {
    this->lista->imprimir();
  }

  void InserirFim(int valor)
  {
    this->lista->inserirInicio(valor);
  }

  int RetirarFim()
  {
    this->lista->RetirarFim();
  }
};

class Fila : public Lista
{
private:
  Lista *lista;

public:
  Fila()
  {
    lista = new Lista();
  };

  ~Fila(){
     delete(lista);
  };

  void InserirFim(int valor)
  {
    this->lista->InserirFim(valor);
    this->setTamanho(lista->getTamanho());
    this->setInicio(lista->getInicio());
    this->setFim(lista->getFim());
  }

  int RemoverInicio()
  {
    int v = this->lista->RemoverInicio();
    this->setTamanho(lista->getTamanho());
    this->setInicio(lista->getInicio());
    this->setFim(lista->getFim());
    return v;
  }

  void imprimir()
  {
    this->lista->imprimir();
  }
};

void TesteLista()
{
  Lista *lista = new Lista();
  int size, i;
  cout << "Digite o tamanho da lista:";
  scanf("%d", &size);
  for (i = 0; i < size; i++)
    lista->inserirInicio(rand() % size);

  lista->imprimir();

  lista->inserirInicio(55);
  printf("Inserido no inicio: 55 \n");

  lista->imprimir();

  lista->InserirFim(66);
  printf("Inserido no fim: 66 \n");

  lista->imprimir();

  lista->removerElemento(1);
  printf("Lista com o primeiro elemento removido: \n");

  lista->imprimir();

  printf("Lista com o quarto elemento removido: \n");

  lista->removerElemento(4);

  lista->imprimir();

 // delete(lista);
};

void TestePilha()
{
  Pilha *pilha = new Pilha();

  int size, i;
  printf("Digite o tamanho da pilha: ");
  scanf("%d", &size);
  for (i = 0; i < size; i++)
    pilha->InserirFim(rand() % size);

  pilha->imprimir();
  printf("Inserido no topo: 99 \n");
  pilha->InserirFim(99);

  pilha->imprimir();

  printf("Retirado do topo: 99 \n");
  pilha->RetirarFim();

  pilha->imprimir();

 // delete(pilha);

}

void TesteFila()
{
  Fila *fila = new Fila();

  int size, i;
  printf("Digite o tamanho da fila: ");
  scanf("%d", &size);
  for (i = 0; i < size; i++)
    fila->InserirFim(rand() % size);

  fila->imprimir();

  fila->InserirFim(55);
  printf("Inserido no fim: 55");

  fila->imprimir();

  fila->InserirFim(66);
  printf("Inserido no fim: 66");

  fila->imprimir();

  for (i = fila->getTamanho(); i > 1; i--)
  {
    printf("Retirado o elemento %d do inicio", fila->RemoverInicio());
    fila->imprimir();
  }

  //delete(fila);
}

int main()
{
  TesteLista();
  printf("---------------------------------------------------------------------------\n");
  TestePilha();
  printf("---------------------------------------------------------------------------\n");
  TesteFila();
}