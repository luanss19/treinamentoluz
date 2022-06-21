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
    printf("\nElementos do array sem serem ordenados:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);
    }
    for (cont = 1; cont < size; cont++)
    {
        for (i = 0; i < size - 1; i++)
        {
            if (arr[i] > arr[i + 1])
            {
                aux = arr[i];
                arr[i] = arr[i + 1];
                arr[i + 1] = aux;
            }
        }
    }
    printf("\n\nElementos do array em ordem crescente:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);

    }
}