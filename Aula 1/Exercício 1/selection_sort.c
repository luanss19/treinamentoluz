#include <stdio.h>
#include <stdlib.h>

void selection_sort(int vetor[], int size)
{
}

void main()
{
    int size;
    printf("Digite o tamanho da lista: \n");
    scanf("%d", &size);

    int arr[size], i, cont, aux, min_index;

    for (i = 0; i < size; i++)
        arr[i] = rand() % size;
    printf("\nElementos do array sem serem ordenados:");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);
    }

    for (cont = 0; cont < (size - 1); cont++)
    {
        min_index = cont;
        for (i = cont + 1; i < size; i++)
        {
            if (arr[i] < arr[min_index])
            {
                min_index = i;
            }
        }
        if (cont != min_index)
        {
            aux = arr[cont];
            arr[cont] = arr[min_index];
            arr[min_index] = aux;
        }
    }

    printf("\n\nElementos do array em ordem crescente:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);

    }
}