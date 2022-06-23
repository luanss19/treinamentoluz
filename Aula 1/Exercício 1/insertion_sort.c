#include <stdio.h>
#include <stdlib.h>


void main()
{
    int size;
    printf("Digite o tamanho da lista: \n");
    scanf("%d", &size);
    int arr[size], i, cont, aux;
    for (i = 0; i < size; i++)
        arr[i] = rand() % size;
    printf("\nElementos do array sem serem ordenados:");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);
    }

    for (cont = 1; cont <= size - 1; cont++){
       printf("\n[%d] ", cont);

       aux = arr[cont];
       i = cont - 1;
       while (i >= 0 && aux < arr[i]) {
          printf("%d, ", i);

          arr[i+1] = arr[i];
          i--;
       }

      arr[i+1] = aux;
   }
    printf("\n\nElementos do array em ordem crescente:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);

    }
}